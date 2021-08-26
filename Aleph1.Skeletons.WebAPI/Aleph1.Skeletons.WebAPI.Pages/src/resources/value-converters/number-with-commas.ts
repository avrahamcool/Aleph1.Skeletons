import { numberWithCommas } from "resources/helpers";

export class NumberWithCommasValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(value: string): string
	{
		return numberWithCommas(value);
	}
}
