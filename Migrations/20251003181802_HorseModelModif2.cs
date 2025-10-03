﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class HorseModelModif2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Horses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Horses");
        }
    }
}
