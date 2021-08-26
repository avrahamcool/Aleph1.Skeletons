import { HttpClient } from "aurelia-fetch-client";
import { minute } from "resources/helpers";
import { default as environment } from "../../../config/environment.json";

export class AuthHttpClient extends HttpClient
{
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

	private inactiveSessionTimeoutHandler: number;

	public startInactiveSessionTimeout(): void
	{
		this.refreshToken();
		this.resetInactiveSessionTimeout();
	}

	private resetInactiveSessionTimeout()
	{
		this.clearInactiveSessionTimeoutHandler();
		this.inactiveSessionTimeoutHandler = window.setTimeout(
			this.refreshToken,
			minute * environment.idleDurationUntilWarningInMin
		);
	}

	public clearInactiveSessionTimeoutHandler(): void
	{
		clearTimeout(this.inactiveSessionTimeoutHandler);
	}

	private refreshToken = () => this.post("/api/refresh-token");

	static queryString(url: string, params: { [key: string]: string | number | boolean | Date }): string
	{
		const query = Object.entries(params)
			.filter(([, value]) => value != null)
			.map(([key, value]) =>
			{
				const valueAsString = value instanceof Date ? value.toJSON() : value.toString();
				return `${key}=${valueAsString}`;
			})
			.join("&");
		return `${url}?${query}`;
	}
}
