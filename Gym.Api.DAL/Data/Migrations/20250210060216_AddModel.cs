using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Api.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exerciseSystemItems_exerciseSystems_ExerciseSystemId",
                table: "exerciseSystemItems");

            migrationBuilder.DropIndex(
                name: "IX_exerciseSystemItems_ExerciseSystemId",
                table: "exerciseSystemItems");

            migrationBuilder.DropColumn(
                name: "ExerciseSystemId",
                table: "exerciseSystemItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseSystemId",
                table: "exerciseSystemItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_exerciseSystemItems_ExerciseSystemId",
                table: "exerciseSystemItems",
                column: "ExerciseSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_exerciseSystemItems_exerciseSystems_ExerciseSystemId",
                table: "exerciseSystemItems",
                column: "ExerciseSystemId",
                principalTable: "exerciseSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
