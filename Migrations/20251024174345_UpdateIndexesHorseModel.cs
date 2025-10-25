using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_course.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexesHorseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Horses_DamId",
                table: "Horses",
                column: "DamId");

            migrationBuilder.CreateIndex(
                name: "IX_Horses_OwnerId_Gender_Age",
                table: "Horses",
                columns: new[] { "OwnerId", "Gender", "Age" });

            migrationBuilder.CreateIndex(
                name: "IX_Horses_SireId",
                table: "Horses",
                column: "SireId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Horses_DamId",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_OwnerId_Gender_Age",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_SireId",
                table: "Horses");
        }
    }
}
