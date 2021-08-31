import { padNumber } from "resources/helpers";

export class PadNumberValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(value: string, maxLen: number = 2): string
	{
		return padNumber(value, maxLen);
	}
}
