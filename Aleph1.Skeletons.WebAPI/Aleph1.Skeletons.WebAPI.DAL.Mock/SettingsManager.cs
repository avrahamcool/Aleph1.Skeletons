using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal static class SettingsManager
	{
		public static readonly DbContextOptions<GenericContextMock> DBOptions = new DbContextOptionsBuilder<GenericContextMock>()
			.UseInMemoryDatabase(databaseName: "Mock")
			.Options;
	}
}
