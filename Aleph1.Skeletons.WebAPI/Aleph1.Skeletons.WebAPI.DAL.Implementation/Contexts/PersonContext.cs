using Aleph1.Skeletons.WebAPI.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	public partial class GenericContext : DbContext
	{
		internal DbSet<Person> Person { get; set; }

		private sealed class PersonConfig : IEntityTypeConfiguration<Person>
		{
			public void Configure(EntityTypeBuilder<Person> builder)
			{
				_ = builder.HasKey(p => p.Id);
			}
		}
	}
}
