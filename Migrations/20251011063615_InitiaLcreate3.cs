using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class InitiaLcreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "PersonalityType",
                schema: "HorseGame_doingdugat",
                newName: "PersonalityType",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Levels",
                schema: "HorseGame_doingdugat",
                newName: "Levels",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Horses_Fears",
                schema: "HorseGame_doingdugat",
                newName: "Horses_Fears",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Horses",
                schema: "HorseGame_doingdugat",
                newName: "Horses",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "HorseBreeds",
                schema: "HorseGame_doingdugat",
                newName: "HorseBreeds",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Competitions_ScaryObject",
                schema: "HorseGame_doingdugat",
                newName: "Competitions_ScaryObject",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Competitions",
                schema: "HorseGame_doingdugat",
                newName: "Competitions",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "PersonalityType",
                schema: "public",
                newName: "PersonalityType",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Levels",
                schema: "public",
                newName: "Levels",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Horses_Fears",
                schema: "public",
                newName: "Horses_Fears",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Horses",
                schema: "public",
                newName: "Horses",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "HorseBreeds",
                schema: "public",
                newName: "HorseBreeds",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Competitions_ScaryObject",
                schema: "public",
                newName: "Competitions_ScaryObject",
                newSchema: "HorseGame_doingdugat");

            migrationBuilder.RenameTable(
                name: "Competitions",
                schema: "public",
                newName: "Competitions",
                newSchema: "HorseGame_doingdugat");
        }
    }
}
