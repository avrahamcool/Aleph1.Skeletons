import { autoinject, computedFrom, PLATFORM } from "aurelia-framework";
import { Router } from "aurelia-router";
import { watch } from "aurelia-watch-decorator";
import { load as loadReCaptcha, ReCaptchaInstance } from "recaptcha-v3";
import { gsap } from "gsap";
import { UserService } from "resources/services";
import { Credentials } from "resources/models";
import { outerHeight } from "resources/helpers";
import { busyTracking, handleErrors } from "resources/decorators";
import { default as environment } from "../../config/environment.json";

@autoinject()
export class SignInShell
{
	constructor(
		public router: Router,
		public userService: UserService
	) { }

	public credentials: Credentials;
	public wrapperElement: HTMLElement;
	public alertsElement: HTMLElement;
	public showAlert: boolean = false;
	public capsLockModifier: boolean = false;
	public captcha: ReCaptchaInstance;

	public activate(): void
	{
		this.credentials = new Credentials();
	}

	public async attached(): Promise<void>
	{
		gsap.to(this.wrapperElement, { height: 0, duration: 0 });
		PLATFORM.addEventListener("keydown", this.detectCapsLockHandler, false);
		await this.loadCaptcha();
	}
	public detached(): void
	{
		PLATFORM.removeEventListener("keydown", this.detectCapsLockHandler, false);
		this.captcha.hideBadge();
	}

	@computedFrom("credentials.username", "credentials.password")
	public get signInDisabled(): boolean
	{
		return !this.credentials.username || !this.credentials.password;
	}

	@handleErrors("התרחשה שגיאה בעת ההזדהות")
	@busyTracking("signInPending")
	public async signIn(): Promise<void>
	{
		this.credentials.captcha = await this.captcha.execute();
		await this.userService.signIn(this.credentials);
		this.credentials.password = "";
	}

	@watch<SignInShell>(x => x.capsLockModifier)
	@watch<SignInShell>(x => x.hebrewModifier)
	private toggleAlerts(): void
	{
		if (this.capsLockModifier || this.hebrewModifier)
		{
			window.setTimeout(() =>
			{
				gsap.to(this.wrapperElement, {
					height: outerHeight(this.alertsElement),
					duration: 0.5,
					ease: "expo.inOut",
					onComplete: () => { this.showAlert = true; }
				});
			}, 10);
		}
		else
		{
			gsap.to(this.wrapperElement, {
				height: 0,
				duration: 0.5,
				ease: "expo.inOut",
				onStart: () => { this.showAlert = false; }
			});
		}
	}

	private detectCapsLockHandler = (event: KeyboardEvent): void =>
	{
		this.capsLockModifier = event.getModifierState("CapsLock");
	};

	@computedFrom("credentials.password")
	private get hebrewModifier()
	{
		return !!this.credentials.password && /[\u05d0-\u05ea]/.test(this.credentials.password);
	}

	private async loadCaptcha(): Promise<void>
	{
		if (this.captcha)
		{
			this.captcha.showBadge();
		}
		else
		{
			this.captcha = await loadReCaptcha(environment.captchaSiteKey, { explicitRenderParameters: { badge: "bottomleft" } });
		}
	}
}
