export class JsonValueConverter
{
	toView(value: unknown): string
	{
		return value ? JSON.stringify(value) : "";
	}
}
