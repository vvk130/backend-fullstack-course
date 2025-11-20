using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class CascadingDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesAds_HorseId",
                table: "SalesAds",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_CompResults_HorseId",
                table: "CompResults",
                column: "HorseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompResults_Animals_HorseId",
                table: "CompResults",
                column: "HorseId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesAds_Animals_HorseId",
                table: "SalesAds",
                column: "HorseId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompResults_Animals_HorseId",
                table: "CompResults");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesAds_Animals_HorseId",
                table: "SalesAds");

            migrationBuilder.DropIndex(
                name: "IX_SalesAds_HorseId",
                table: "SalesAds");

            migrationBuilder.DropIndex(
                name: "IX_CompResults_HorseId",
                table: "CompResults");
        }
    }
}
