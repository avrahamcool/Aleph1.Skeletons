import { AuthenticationInfo } from "../models/authentication-info";
import { computedFrom, autoinject } from "aurelia-framework";
import * as ExpiredStorage from "expired-storage";
import * as environment from "../../../config/environment.json";

const SECONDS_IN_MINUTE = 60;

@autoinject
export class UserService
{
	constructor(private expiredStorage: ExpiredStorage) { }

	private _authenticationInfo: AuthenticationInfo;

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
		this.expiredStorage.setJson("_authenticationInfo", value, SECONDS_IN_MINUTE * environment.idleDurationUntilWarningMin);
	}

	@computedFrom("authenticationInfo")
	public get isLoggedIn(): boolean
	{
		return !!this.authenticationInfo;
	}
}
