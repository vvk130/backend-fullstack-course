using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FearType_Horses_HorseId",
                table: "FearType");

            migrationBuilder.DropTable(
                name: "PersonalityType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Horses",
                table: "Horses");

            migrationBuilder.RenameTable(
                name: "Horses",
                newName: "Animal");

            migrationBuilder.RenameIndex(
                name: "IX_Horses_SireId",
                table: "Animal",
                newName: "IX_Animal_SireId");

            migrationBuilder.RenameIndex(
                name: "IX_Horses_OwnerId_Gender_Age",
                table: "Animal",
                newName: "IX_Animal_OwnerId_Gender_Age");

            migrationBuilder.RenameIndex(
                name: "IX_Horses_DamId",
                table: "Animal",
                newName: "IX_Animal_DamId");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Strength",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Stamina",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Speed",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_JumpingAbility",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Intelligence",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Endurance",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Agility",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Breed",
                table: "Animal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Animal",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Alpacas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<double>(type: "double precision", nullable: false),
                    AlpacaColor = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    AlpacaBreed = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Relationship = table.Column<int>(type: "integer", nullable: false),
                    Energy = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    SireId = table.Column<Guid>(type: "uuid", nullable: true),
                    DamId = table.Column<Guid>(type: "uuid", nullable: true),
                    AlpacaQualities_Agility = table.Column<int>(type: "integer", nullable: false),
                    AlpacaQualities_Intelligence = table.Column<int>(type: "integer", nullable: false),
                    AlpacaQualities_JumpingAbility = table.Column<int>(type: "integer", nullable: false),
                    AlpacaQualities_Speed = table.Column<int>(type: "integer", nullable: false),
                    AlpacaQualities_WoolQuality = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alpacas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animal_Personalities",
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
                    table.PrimaryKey("PK_Animal_Personalities", x => new { x.AnimalId, x.Id });
                    table.ForeignKey(
                        name: "FK_Animal_Personalities_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alpacas_Personalities",
                columns: table => new
                {
                    AlpacaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonalityTrait = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alpacas_Personalities", x => new { x.AlpacaId, x.Id });
                    table.ForeignKey(
                        name: "FK_Alpacas_Personalities_Alpacas_AlpacaId",
                        column: x => x.AlpacaId,
                        principalTable: "Alpacas",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FearType_Animal_HorseId",
                table: "FearType");

            migrationBuilder.DropTable(
                name: "Alpacas_Personalities");

            migrationBuilder.DropTable(
                name: "Animal_Personalities");

            migrationBuilder.DropTable(
                name: "Alpacas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Horses");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_SireId",
                table: "Horses",
                newName: "IX_Horses_SireId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_OwnerId_Gender_Age",
                table: "Horses",
                newName: "IX_Horses_OwnerId_Gender_Age");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_DamId",
                table: "Horses",
                newName: "IX_Horses_DamId");

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Strength",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Stamina",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Speed",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_JumpingAbility",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Intelligence",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Endurance",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Qualities_Agility",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Breed",
                table: "Horses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Horses",
                table: "Horses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonalityType",
                columns: table => new
                {
                    HorseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discovered = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalityTrait = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_FearType_Horses_HorseId",
                table: "FearType",
                column: "HorseId",
                principalTable: "Horses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
