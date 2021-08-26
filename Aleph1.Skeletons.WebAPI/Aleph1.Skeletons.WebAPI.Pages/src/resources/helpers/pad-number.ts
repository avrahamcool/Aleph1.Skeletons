export function padNumber(x: number | string, maxLen: number = 2): string
{
	if (x === undefined)
	{
		return "";
	}

	return x.toString().padStart(maxLen, "0");
}
