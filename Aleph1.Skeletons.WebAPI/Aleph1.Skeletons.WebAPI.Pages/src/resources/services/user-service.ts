import { Router } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";
import { IdleModal } from "components/idle-modal/idle-modal";
import { minute, secondsInMinute } from "./time";
import { AuthHttpClient } from ".";
import { Roles, AuthenticationInfo, LoginModel } from "../models";
import { computedFrom, autoinject, Aurelia } from "aurelia-framework";
import * as ExpiredStorage from "expired-storage";
import * as environment from "../../../config/environment.json";
import { IdleSessionTimeout } from "idle-session-timeout";
import { json } from "aurelia-fetch-client";
import { DialogService } from "aurelia-dialog";
import { load } from "recaptcha-v3";

@autoinject()
export class UserService
{
	private _authenticationInfo: AuthenticationInfo;
	private idleGUITimeout: IdleSessionTimeout;

	constructor(private au: Aurelia, private httpClient: AuthHttpClient,
		private expiredStorage: ExpiredStorage, private dialogService: DialogService,
		private router: Router)
	{ }

	@computedFrom("_authenticationInfo")
	public get authenticationInfo(): AuthenticationInfo
	{
		if (!this._authenticationInfo)
		{
			this._authenticationInfo = this.expiredStorage.getJson("_authenticationInfo");
		}
		return this._authenticationInfo;
	}
	public set authenticationInfo(value: AuthenticationInfo)
	{
		this._authenticationInfo = value;
		this.expiredStorage.setJson("_authenticationInfo", value, secondsInMinute * environment.idleDurationUntilWarningMin);
	}
	@computedFrom("authenticationInfo")
	public get isLoggedIn(): boolean
	{
		return !!this.authenticationInfo;
	}

	public isAllowedForRole(roles: Roles): boolean
	{
		if (roles === Roles.None)
		{
			return true;
		}
		if (!this.authenticationInfo?.roles)
		{
			return false;
		}
		return (this.authenticationInfo.roles & roles) === roles;
	}

	public startIdleTimeout(): void
	{
		if (!this.idleGUITimeout)
		{
			this.idleGUITimeout = new IdleSessionTimeout(minute * environment.idleDurationUntilWarningMin);
			this.idleGUITimeout.onTimeOut = () =>
			{
				this.dialogService.open({ viewModel: IdleModal })
					.whenClosed(result =>
					{
						if (result.wasCancelled)
						{
							this.logout();
						}
						else
						{
							this.idleGUITimeout.start();
						}
					});
			};
		}

		this.idleGUITimeout.start();
		this.httpClient.startInactiveSessionTimeout();
	}

	public async login(credentials: LoginModel): Promise<void>
	{
		const captcha = await load(environment.captchaSiteKey);
		credentials.captchaToken = await captcha.execute();
		const resp = await this.httpClient.post("/api/login", json(credentials));
		const authInfo = await resp.json();
		this.authenticationInfo = authInfo;
		await this.au.setRoot(PLATFORM.moduleName("shells/app"));

		captcha.hideBadge();
		return this.startIdleTimeout();
	}

	public logout(): Promise<void>
	{
		return this.httpClient.post("/api/logout")
			.then(() => this.authenticationInfo = null)
			.then(() => this.router.navigate("", { replace: true, trigger: false }))
			.then(() => this.au.setRoot(PLATFORM.moduleName("shells/login")))
			.then(() =>
			{
				this.httpClient.clearInactiveSessionTimeoutHandler();
				this.idleGUITimeout.dispose();
			})
			.then(() => load(environment.captchaSiteKey))
			.then(captcha => captcha.showBadge());
	}
}
