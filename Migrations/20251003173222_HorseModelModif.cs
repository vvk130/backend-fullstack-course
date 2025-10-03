using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class HorseModelModif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Breed = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fear",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    FearType = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    HorseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fear_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTrait",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    HorseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTrait", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalityTrait_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fear_HorseId",
                table: "Fear",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityTrait_HorseId",
                table: "PersonalityTrait",
                column: "HorseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fear");

            migrationBuilder.DropTable(
                name: "PersonalityTrait");

            migrationBuilder.DropTable(
                name: "Horses");
        }
    }
}
