export function padNumber(x: number | string, maxLen: number = 2): string
{
	if (x || x === 0)
	{
		return x.toString().padStart(maxLen, "0");
	}

	return "";
}
