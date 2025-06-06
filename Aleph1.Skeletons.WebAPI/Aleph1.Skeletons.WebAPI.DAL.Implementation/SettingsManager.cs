using System.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	internal static class SettingsManager
	{
		public static readonly DbContextOptions<GenericContext> DBOptions = new DbContextOptionsBuilder<GenericContext>()
				.UseSqlServer(GenericContextConnectionString)
				.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
				.Options;


		private static string _genericContextConnectionString;
		public static string GenericContextConnectionString
		{
			get
			{
				if (_genericContextConnectionString == default)
				{
					_genericContextConnectionString = ConfigurationManager.AppSettings["GenericContextConnectionString"];
				}
				return _genericContextConnectionString;
			}
		}
	}

	internal sealed class GenericContextFactory : IDesignTimeDbContextFactory<GenericContext>
	{
		public GenericContext CreateDbContext(string[] args)
		{
			return new GenericContext(SettingsManager.DBOptions);
		}
	}
}
