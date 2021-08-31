import { default as DOMPurify } from "dompurify";

export class PurifyHTMLValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(value: string, allowedTags: string[]): string
	{
		if (value)
		{
			return DOMPurify.sanitize(value, { ALLOWED_TAGS: allowedTags });
		}
		return "";
	}
}
