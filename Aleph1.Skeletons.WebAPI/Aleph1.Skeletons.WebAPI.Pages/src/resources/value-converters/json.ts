export class JsonValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(value: unknown): string
	{
		return value ? JSON.stringify(value) : "";
	}
}
