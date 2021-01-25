using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;

using Aleph1.Skeletons.WebAPI.Models.Entities;

using TrackerEnabledDbContext;
using TrackerEnabledDbContext.Common.Configuration;
using TrackerEnabledDbContext.Common.Models;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
	internal class GenericContext : TrackerContext
	{
		// Your context has been configured to use a 'GenericContext' connection string from your application's 
		// configuration file (Web.config). By default, this connection string targets the 
		// 'Aleph1.Skeletons.WebAPI.DAL.PersonContext' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'GenericContext' 
		// connection string in the application configuration file.
		public GenericContext() : base("name=GenericContext")
		{
			Configuration.ProxyCreationEnabled = false;
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Contract.Requires(modelBuilder != null);

			// setting default length of all strings instead of VarChar(max) - each property can be overwritten later
			modelBuilder.Properties<string>().Configure(s => s.IsUnicode(false).HasMaxLength(256));

			#region Logs
			modelBuilder.Entity<AuditLog>().ToTable("LOG_AuditLogs");

			EntityTypeConfiguration<AuditLogDetail> auditLogBuilder = modelBuilder.Entity<AuditLogDetail>().ToTable("LOG_AuditLogDetails");
			auditLogBuilder.Property(l => l.OriginalValue).HasMaxLength(2000);
			auditLogBuilder.Property(l => l.NewValue).HasMaxLength(2000);

			modelBuilder.Entity<LogMetadata>().ToTable("LOG_LogMetadata");
			#endregion

			EntityTypeConfiguration<Person> personConfiguration = modelBuilder.Entity<Person>().ToTable("TBL_Persons");
			EntityTracker.TrackAllProperties<Person>();
		}
	}
}