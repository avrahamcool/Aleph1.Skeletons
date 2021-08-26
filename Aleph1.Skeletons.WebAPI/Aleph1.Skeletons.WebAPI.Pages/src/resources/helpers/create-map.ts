export function createMap<T extends string, V = boolean>(names: readonly T[]): Record<T, V>;

export function createMap<T extends string, V>(names: readonly T[], Instance?: new () => V): Record<T, V>;

export function createMap<T extends string, V>(names: readonly T[], Instance?: new () => V): Record<T, V>
{
	return names.reduce((acc, curr) =>
	{
		if (Instance)
		{
			try
			{
				const x = new Instance();
				acc[curr] = x;
			}
			catch
			{
				acc[curr] = undefined as unknown as V;
			}
		}
		else
		{
			acc[curr] = false as unknown as V;
		}
		return acc;
	}, {} as Record<T, V>);
}
