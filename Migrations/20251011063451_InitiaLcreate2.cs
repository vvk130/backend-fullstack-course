using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class InitiaLcreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "PersonalityType",
                newName: "PersonalityType",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Levels",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Horses_Fears",
                newName: "Horses_Fears",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Horses",
                newName: "Horses",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "HorseBreeds",
                newName: "HorseBreeds",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Competitions_ScaryObject",
                newName: "Competitions_ScaryObject",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Competitions",
                newName: "Competitions",
                newSchema: "HorseGame_doingdugat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonalityType",
                schema: "HorseGame_doingdugat",
                newName: "PersonalityType");

            migrationBuilder.RenameTable(
                name: "Levels",
                schema: "HorseGame_doingdugat",
                newName: "Levels");

            migrationBuilder.RenameTable(
                name: "Horses_Fears",
                schema: "HorseGame_doingdugat",
                newName: "Horses_Fears");

            migrationBuilder.RenameTable(
                name: "Horses",
                schema: "HorseGame_doingdugat",
                newName: "Horses");

            migrationBuilder.RenameTable(
                name: "HorseBreeds",
                schema: "HorseGame_doingdugat",
                newName: "HorseBreeds");

            migrationBuilder.RenameTable(
                name: "Competitions_ScaryObject",
                schema: "HorseGame_doingdugat",
                newName: "Competitions_ScaryObject");

            migrationBuilder.RenameTable(
                name: "Competitions",
                schema: "HorseGame_doingdugat",
                newName: "Competitions");
        }
    }
}
