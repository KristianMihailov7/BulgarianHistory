using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulgarianHistory.Data.Migrations
{
    public partial class ProblemSolving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "FamousPeople");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "FamousPeople");

            migrationBuilder.RenameColumn(
                name: "Biography",
                table: "FamousPeople",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "FamousPeople",
                newName: "Biography");

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "FamousPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeathYear",
                table: "FamousPeople",
                type: "int",
                nullable: true);
        }
    }
}
