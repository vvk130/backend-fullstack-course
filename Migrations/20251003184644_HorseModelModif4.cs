using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class HorseModelModif4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qualities_Agility",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qualities_Endurance",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qualities_Intelligence",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qualities_Speed",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qualities_Stamina",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qualities_Strength",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualities_Agility",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "Qualities_Endurance",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "Qualities_Intelligence",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "Qualities_Speed",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "Qualities_Stamina",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "Qualities_Strength",
                table: "Horses");
        }
    }
}
