import { default as format } from "date-fns/format";

export class DateFormatValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(value: Date, pattern = "dd/MM/yyyy"): string
	{
		if (value)
		{
			return format(value, pattern);
		}
		return "";
	}
}
