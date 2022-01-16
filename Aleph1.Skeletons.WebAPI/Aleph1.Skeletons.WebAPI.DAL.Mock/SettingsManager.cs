using Aleph1.Skeletons.WebAPI.DAL.Implementation;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal static class SettingsManager
	{
		public static readonly DbContextOptions<GenericContext> DBOptions = new DbContextOptionsBuilder<GenericContext>()
			.UseInMemoryDatabase(databaseName: "Mock")
			.Options;
	}
}
