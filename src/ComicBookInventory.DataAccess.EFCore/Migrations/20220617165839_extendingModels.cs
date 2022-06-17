using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicBookInventory.DataAccess.EFCore.Migrations
{
    public partial class extendingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlive",
                table: "Characters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryAbility",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAbility",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weapon",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "IsAlive",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PrimaryAbility",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SecondaryAbility",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Weapon",
                table: "Characters");
        }
    }
}
