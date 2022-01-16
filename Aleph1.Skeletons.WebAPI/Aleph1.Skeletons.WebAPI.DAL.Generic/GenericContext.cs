using System.Diagnostics.Contracts;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Aleph1.Skeletons.WebAPI.DAL.Generic
{
	/// <summary>generic context base</summary>
	public partial class GenericContext : DbContext
	{
		/// <summary></summary>
		public GenericContext(DbContextOptions<GenericContext> options) : base(options) { }

		/// <summary>make global configurations, and load all contexts</summary>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Contract.Requires(modelBuilder != null);

			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

			foreach (IMutableProperty property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()))
			{
				if (property.ClrType == typeof(string) && !property.IsUnicode().HasValue)
				{
					property.SetIsUnicode(false);
				}
			}
		}
	}
}
