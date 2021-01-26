import { Roles } from "./roles";

export interface AuthenticationInfo
{
	roles: Roles;
	username: string;
}
