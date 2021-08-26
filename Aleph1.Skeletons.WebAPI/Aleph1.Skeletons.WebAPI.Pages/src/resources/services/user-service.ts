import { computedFrom, autoinject, Aurelia } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";
import { Router } from "aurelia-router";
import { DialogService } from "aurelia-dialog";
import { serialize } from "class-transformer";
import { IdleSessionTimeout } from "idle-session-timeout";
import { default as ExpiredStorage } from "expired-storage";
import { load as loadCaptcha } from "recaptcha-v3";
import { Identity, Credentials } from "resources/models";
import { Roles } from "resources/enums";
import { minute, secondsInMinute } from "resources/helpers";
import { AuthHttpClient } from "resources/services";
import { IdleModal } from "components/idle-modal/idle-modal";
import { default as environment } from "../../../config/environment.json";

@autoinject()
export class UserService
{
	private _identity: Identity | null;
	private idleSessionTimeout: IdleSessionTimeout;

	constructor(
		private au: Aurelia,
		private httpClient: AuthHttpClient,
		private expiredStorage: ExpiredStorage,
		private dialogService: DialogService,
		private router: Router
	) { }

	@computedFrom("_identity")
	public get identity(): Identity | null
	{
		if (!this._identity)
		{
			this._identity = this.expiredStorage.getJson("_identity");
		}
		return this._identity;
	}
	public set identity(value: Identity | null)
	{
		this._identity = value;
		this.expiredStorage.setJson("_identity", value ?? null as any, secondsInMinute * environment.idleDurationUntilWarningInMin);
	}

	@computedFrom("identity")
	public get isSignedIn(): boolean
	{
		return !!this.identity;
	}

	public isAllowedForRole(roles: Roles): boolean
	{
		if (roles === Roles.None)
		{
			return true;
		}
		if (!this.identity?.roles)
		{
			return false;
		}
		return (this.identity?.roles & roles) === roles;
	}

	public startIdleTimeout(): void
	{
		if (!this.idleSessionTimeout)
		{
			this.idleSessionTimeout = new IdleSessionTimeout(
				minute * environment.idleDurationUntilWarningInMin
			);
			this.idleSessionTimeout.onTimeOut = () =>
			{
				this.dialogService.open({ viewModel: IdleModal })
					.whenClosed(result =>
					{
						if (result.wasCancelled)
						{
							this.signOut();
						}
						else
						{
							this.idleSessionTimeout.start();
						}
					});
			};
		}

		this.idleSessionTimeout.start();
		this.httpClient.startInactiveSessionTimeout();
	}

	public async signIn(credentials: Credentials): Promise<string>
	{
		const captcha = await loadCaptcha(environment.captchaSiteKey, { explicitRenderParameters: { badge: "bottomleft" } });
		credentials.reCaptcha = await captcha.execute();
		const response = await this.httpClient.post("/api/sign-in", serialize(credentials));
		const secret = await response.json();
		captcha.hideBadge();
		return secret;
	}

	public async signOut(): Promise<void>
	{
		try
		{
			await this.httpClient.post("/api/sign-out");
		}
		finally
		{
			this.identity = null;
			this.httpClient.clearInactiveSessionTimeoutHandler();
			this.idleSessionTimeout.dispose();
			this.dialogService.closeAll();
			this.router.navigate("", { replace: true, trigger: false });
			this.au.setRoot(PLATFORM.moduleName("shells/sign-in"));
			const captcha = await loadCaptcha(environment.captchaSiteKey, { explicitRenderParameters: { badge: "bottomleft" } });
			captcha.showBadge();
		}
	}
}
