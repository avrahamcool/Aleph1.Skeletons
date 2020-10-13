import { minute } from ".";
import { HttpClient } from "aurelia-fetch-client";
import * as environment from "../../../config/environment.json";

export class AuthHttpClient extends HttpClient
{
	private inactiveSessionTimeoutHandler: number;
	public startInactiveSessionTimeout(): void
	{
		this.refreshToken();
		this.resetInactiveSessionTimeout();
	}
	private resetInactiveSessionTimeout()
	{
		this.clearInactiveSessionTimeoutHandler();
		this.inactiveSessionTimeoutHandler = window.setTimeout(this.refreshToken, minute * environment.idleDurationUntilWarningMin);
	}
	public clearInactiveSessionTimeoutHandler(): void
	{
		clearTimeout(this.inactiveSessionTimeoutHandler);
	}
	private refreshToken = () => this.post("/api/refresh-token");

	constructor()
	{
		super();
		this.configure(config =>
		{
			config.useStandardConfiguration()
				.withDefaults({ credentials: "include" })
				.withBaseUrl(environment.apiBaseUrl)
				.withInterceptor({
					response: response =>
					{
						this.resetInactiveSessionTimeout();
						return response;
					}
				});
		});
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
