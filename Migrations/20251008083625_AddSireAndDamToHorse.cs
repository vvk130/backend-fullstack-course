using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class AddSireAndDamToHorse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Horses",
                newName: "OwnerId");

            migrationBuilder.AddColumn<Guid>(
                name: "DamId",
                table: "Horses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SireId",
                table: "Horses",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamId",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "SireId",
                table: "Horses");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Horses",
                newName: "ownerId");
        }
    }
}
