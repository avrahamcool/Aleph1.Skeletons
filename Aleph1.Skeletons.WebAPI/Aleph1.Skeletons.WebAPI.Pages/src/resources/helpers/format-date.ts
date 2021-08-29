import { format, parseISO } from "date-fns";

export function formatDate(date: Date | string, layout: string = "dd/MM/yyyy"): string
{
	try
	{
		if (typeof date === "string")
		{
			return format(parseISO(date as any as string), layout);
		}
		return format(date, layout);
	}
	catch
	{
		return "";
	}
}
