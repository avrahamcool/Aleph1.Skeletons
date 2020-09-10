import { AuthenticationInfo } from "../models/authentication-info";
import { computedFrom } from "aurelia-framework";

export class UserService
{
	private _authenticationInfo: AuthenticationInfo;

	@computedFrom("_authenticationInfo")
	public get authenticationInfo(): AuthenticationInfo
	{
		if (!this._authenticationInfo)
		{
			const temp = localStorage.getItem("_authenticationInfo");
			this._authenticationInfo = temp && JSON.parse(temp);
		}
		return this._authenticationInfo;
	}

	public set authenticationInfo(value: AuthenticationInfo)
	{
		this._authenticationInfo = value;
		localStorage.setItem("_authenticationInfo", JSON.stringify(value));
	}

	@computedFrom("authenticationInfo")
	public get isLoggedIn(): boolean
	{
		return !!this.authenticationInfo;
	}
}
