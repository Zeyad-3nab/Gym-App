using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Api.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exerciseSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseSystemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exerciseSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exerciseSystems_exerciseSystems_ExerciseSystemId",
                        column: x => x.ExerciseSystemId,
                        principalTable: "exerciseSystems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "exerciseSystemItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ExerciseSystemId = table.Column<int>(type: "int", nullable: false),
                    NumOfGroups = table.Column<int>(type: "int", nullable: false),
                    NumOfCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exerciseSystemItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exerciseSystemItems_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exerciseSystemItems_exerciseSystems_ExerciseSystemId",
                        column: x => x.ExerciseSystemId,
                        principalTable: "exerciseSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exerciseSystemItems_ExerciseId",
                table: "exerciseSystemItems",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_exerciseSystemItems_ExerciseSystemId",
                table: "exerciseSystemItems",
                column: "ExerciseSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_exerciseSystems_ExerciseSystemId",
                table: "exerciseSystems",
                column: "ExerciseSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exerciseSystemItems");

            migrationBuilder.DropTable(
                name: "exerciseSystems");
        }
    }
}
