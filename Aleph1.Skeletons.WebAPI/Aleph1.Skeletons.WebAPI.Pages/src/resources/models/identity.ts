import { Roles } from "../enums/roles";

export interface Identity
{
	roles: Roles;
	username: string;
}
