import format from "date-fns/format";

export class DateFormatValueConverter
{
	toView(value: Date, pattern = "dd/MM/yyyy"): string
	{
		return format(value, pattern);
	}
}
