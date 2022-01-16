using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation.Migrations
{
	internal partial class Person : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
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
					table.PrimaryKey("PK_Person", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Person");
		}
	}
}
