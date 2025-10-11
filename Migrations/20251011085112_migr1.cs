using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class migr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionType = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

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
                name: "Horses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<double>(type: "double precision", nullable: false),
                    Color = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Breed = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Relationship = table.Column<int>(type: "integer", nullable: false),
                    Energy = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    SireId = table.Column<Guid>(type: "uuid", nullable: true),
                    DamId = table.Column<Guid>(type: "uuid", nullable: true),
                    Qualities_Agility = table.Column<int>(type: "integer", nullable: false),
                    Qualities_Endurance = table.Column<int>(type: "integer", nullable: false),
                    Qualities_Intelligence = table.Column<int>(type: "integer", nullable: false),
                    Qualities_JumpingAbility = table.Column<int>(type: "integer", nullable: false),
                    Qualities_Speed = table.Column<int>(type: "integer", nullable: false),
                    Qualities_Stamina = table.Column<int>(type: "integer", nullable: false),
                    Qualities_Strength = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.Id);
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
                    Stable_StableType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions_ScaryObject",
                columns: table => new
                {
                    CompetitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FearItem = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions_ScaryObject", x => new { x.CompetitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Competitions_ScaryObject_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horses_Fears",
                columns: table => new
                {
                    HorseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FearItem = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses_Fears", x => new { x.HorseId, x.Id });
                    table.ForeignKey(
                        name: "FK_Horses_Fears_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityType",
                columns: table => new
                {
                    HorseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonalityTrait = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityType", x => new { x.HorseId, x.Id });
                    table.ForeignKey(
                        name: "FK_PersonalityType_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competitions_ScaryObject");

            migrationBuilder.DropTable(
                name: "HorseBreeds");

            migrationBuilder.DropTable(
                name: "Horses_Fears");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "PersonalityType");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Horses");
        }
    }
}
