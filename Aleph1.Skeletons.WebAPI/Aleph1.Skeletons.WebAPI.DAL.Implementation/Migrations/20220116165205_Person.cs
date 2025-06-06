using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation.Migrations
{
	internal sealed partial class Person : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			_ = migrationBuilder.CreateTable(
				name: "Person",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(unicode: false, nullable: true),
					LastName = table.Column<string>(unicode: false, nullable: true),
					BirthDate = table.Column<DateTimeOffset>(nullable: false)
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Person", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			_ = migrationBuilder.DropTable(
				name: "Person");
		}
	}
}
