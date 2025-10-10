using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class AddBreedsAndLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorseBreeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Breed = table.Column<int>(type: "integer", nullable: false),
                    MinHeightCm = table.Column<int>(type: "integer", nullable: false),
                    MaxHeightCm = table.Column<int>(type: "integer", nullable: false),
                    PossibleColors = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorseBreeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelNumber = table.Column<int>(type: "integer", nullable: false),
                    EntryPoints = table.Column<int>(type: "integer", nullable: false),
                    Stable_Cleanleness = table.Column<int>(type: "integer", nullable: false),
                    Stable_Description = table.Column<string>(type: "text", nullable: false),
                    Stable_EnvironmentScore = table.Column<int>(type: "integer", nullable: false),
                    Stable_ImgUrl = table.Column<string>(type: "text", nullable: false),
                    Stable_StableAmount = table.Column<int>(type: "integer", nullable: false),
                    Stable_StableSlots = table.Column<int>(type: "integer", nullable: false),
                    Stable_StableType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorseBreeds");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
