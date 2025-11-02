using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FearType_Animal_HorseId",
                table: "FearType");

            migrationBuilder.DropTable(
                name: "Animal_Personalities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_SireId",
                table: "Animals",
                newName: "IX_Animals_SireId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_OwnerId_Gender_Age",
                table: "Animals",
                newName: "IX_Animals_OwnerId_Gender_Age");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_DamId",
                table: "Animals",
                newName: "IX_Animals_DamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonalityType",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonalityTrait = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityType", x => new { x.AnimalId, x.Id });
                    table.ForeignKey(
                        name: "FK_PersonalityType_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FearType_Animals_HorseId",
                table: "FearType",
                column: "HorseId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FearType_Animals_HorseId",
                table: "FearType");

            migrationBuilder.DropTable(
                name: "PersonalityType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_SireId",
                table: "Animal",
                newName: "IX_Animal_SireId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_OwnerId_Gender_Age",
                table: "Animal",
                newName: "IX_Animal_OwnerId_Gender_Age");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_DamId",
                table: "Animal",
                newName: "IX_Animal_DamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Animal_Personalities",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalityTrait = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal_Personalities", x => new { x.AnimalId, x.Id });
                    table.ForeignKey(
                        name: "FK_Animal_Personalities_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FearType_Animal_HorseId",
                table: "FearType",
                column: "HorseId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
