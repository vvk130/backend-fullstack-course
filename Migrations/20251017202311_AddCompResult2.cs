using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class AddCompResult2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HorseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ranking = table.Column<int>(type: "integer", nullable: false),
                    MoneyWon = table.Column<double>(type: "double precision", nullable: false),
                    CompetitionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompResults");
        }
    }
}
