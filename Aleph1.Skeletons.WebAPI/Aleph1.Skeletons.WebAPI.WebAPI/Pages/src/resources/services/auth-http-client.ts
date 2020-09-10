import { UserService } from "resources";
import { autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { getLogger, Logger } from "aurelia-logging";
import * as environment from "../../../config/environment.json";

const SECOND = 1000;
const MINUTE = 60 * SECOND;

@autoinject
export class AuthHttpClient extends HttpClient
{
	private idleHandler: number;
	public logger: Logger;
	constructor(private userService: UserService)
	{
		super();
		this.logger = getLogger("AuthHttpClient");
		this.configure(config =>
		{
			config.useStandardConfiguration()
				.withDefaults({ credentials: "include" })
				.withBaseUrl(environment.apiBaseUrl)
				.withInterceptor({
					response: response =>
					{
						this.setIdleTimeout();
						return response;
					},
					responseError: (errorResponse: Response) =>
					{
						// Network Error
						if (errorResponse instanceof TypeError)
						{
							this.displayError("שגיאת תקשורת - אנא נסה שוב מאוחר יותר", errorResponse.message);
							throw errorResponse;
						}

						const clone = errorResponse.clone();

						// Validation errors
						if (errorResponse.status === 400)
						{
							return errorResponse.text()
								.then(text =>
								{
									try
									{
										return JSON.parse(text);
									}
									catch (err)
									{
										return text;
									}
								})
								.then(errorObj =>
								{
									const messageForUser = errorObj.ModelState ? Object.values(errorObj.ModelState)[0][0] : errorObj;
									this.displayError(messageForUser, errorObj);
									throw clone;
								});
						}

						// Any other Error
						return errorResponse.json().then(json => ({ messageForUser: json.ExceptionMessage || json.Message || json, error: json }))
							.catch(() => errorResponse.text().then(text => ({ messageForUser: "התרחשה שגיאה", error: text })))
							.then(customErrorObj =>
							{
								this.displayError(customErrorObj.messageForUser, customErrorObj.error);
								throw clone;
							});
					}
				});
		});
		this.setIdleTimeout();
		this.refreshToken();	//start with a refresh token so all idle timeouts in sync
	}

	private setIdleTimeout()
	{
		clearTimeout(this.idleHandler);
		this.idleHandler = window.setTimeout(() => this.refreshToken(), MINUTE * environment.idleDurationUntilWarningMin);
	}
	private refreshToken()
	{
		if (this.userService.isLoggedIn)
		{
			this.fetch("/api/RefreshToken");
		}
	}

	private displayError(messageForUser: string, error: any): void
	{

		//this.notyf = new Notyf({ position: { x: "left", y: "bottom" }, duration: 5000, dismissible: true });

		this.logger.error(messageForUser, error);
		//this.notyf.error(messageForUser);
	}

	public queryString(baseUrl: string, parameters: { [key: string]: string | number | boolean | Date }): string
	{
		const query = Object.entries(parameters)
			.filter(([, value]) => value != null)
			.map(([key, value]) =>
			{
				const valueAsString = value instanceof Date ? value.toJSON() : value.toString();
				return `${ key }=${ valueAsString }`;
			})
			.join("&");
		return `${ baseUrl }?${ query }`;
	}
}
