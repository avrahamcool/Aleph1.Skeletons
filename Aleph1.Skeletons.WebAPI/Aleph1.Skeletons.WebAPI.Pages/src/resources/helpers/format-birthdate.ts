import { format, parseISO } from "date-fns";

export function formatBirthdate(birthdate: Date | string): string
{
	try
	{
		if (typeof birthdate === "string")
		{
			return format(parseISO(birthdate as any as string), "dd/MM/yyyy");
		}
		return format(birthdate, "dd/MM/yyyy");
	}
	catch
	{
		return "";
	}
}
