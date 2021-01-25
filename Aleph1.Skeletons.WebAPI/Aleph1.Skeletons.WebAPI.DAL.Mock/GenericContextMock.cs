using System.Diagnostics.Contracts;

using Aleph1.Skeletons.WebAPI.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
	internal class GenericContextMock : DbContext
	{
		public DbSet<Person> Persons { get; set; }

		public GenericContextMock(DbContextOptions<GenericContextMock> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Contract.Requires(modelBuilder != null);

			modelBuilder.Entity<Person>().HasKey(p => p.ID);
		}
	}
}
