import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { AuthenticationInfo, AuthHttpClient, UserService } from "resources";
import { json } from "aurelia-fetch-client";
import { LoginModel } from "./login-model";
import { IdleSessionTimeout } from "idle-session-timeout";
import { DialogService } from "aurelia-dialog";
import { Idle } from "./idle/idle";
import * as environment from "../../../config/environment.json";

const MINUTE = 60 * 1000;

@autoinject
export class LoginService
{
	private idleTimeout: IdleSessionTimeout;

	constructor(private httpClient: AuthHttpClient, private userService: UserService, private router: Router, dialogService: DialogService)
	{
		this.idleTimeout = new IdleSessionTimeout(MINUTE * environment.idleDurationUntilWarningMin);
		this.idleTimeout.onTimeOut = () =>
		{
			dialogService.open({ viewModel: Idle })
				.whenClosed(result =>
				{
					if (result.wasCancelled)
					{
						this.logout();
					}
					else
					{
						this.idleTimeout.reset();
					}
				});
		}
		if (this.userService.isLoggedIn)
		{
			this.idleTimeout.start();
		}
	}
	public login(credentials: LoginModel): Promise<void>
	{
		return this.httpClient.post("/api/Login", json(credentials))
			.then(resp => resp.json())
			.then((authInfo: AuthenticationInfo) => this.userService.authenticationInfo = authInfo)
			.then(() =>
			{
				const url = this.userService.redirectAfterLogin || "persons";
				this.userService.redirectAfterLogin = undefined;
				this.router.navigate(url);
			})
			.then(() => this.idleTimeout.start());
	}

	public logout(): Promise<void>
	{
		return this.httpClient.post("/api/Logout")
			.then(() => this.userService.authenticationInfo = null)
			.then(() => this.router.navigateToRoute("login"))
			.then(() => this.idleTimeout.dispose());
	}
}
