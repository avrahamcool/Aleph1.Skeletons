export class ArrayLengthValueConverter
{
	// eslint-disable-next-line class-methods-use-this
	public toView(arr: unknown[]): number
	{
		return arr?.length || 0;
	}
}
