using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Api.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetMuscle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_targetMuscles_TargetMuscleId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_targetMuscles_TargetMuscleId1",
                table: "Exercise");

            migrationBuilder.DropTable(
                name: "targetMuscles");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TargetMuscleId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_TargetMuscleId1",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "TargetMuscleId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "TargetMuscleId1",
                table: "Exercise");

            migrationBuilder.AddColumn<string>(
                name: "TargetMuscle",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetMuscle",
                table: "Exercise");

            migrationBuilder.AddColumn<int>(
                name: "TargetMuscleId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetMuscleId1",
                table: "Exercise",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "targetMuscles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_targetMuscles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TargetMuscleId",
                table: "Exercise",
                column: "TargetMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TargetMuscleId1",
                table: "Exercise",
                column: "TargetMuscleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_targetMuscles_TargetMuscleId",
                table: "Exercise",
                column: "TargetMuscleId",
                principalTable: "targetMuscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_targetMuscles_TargetMuscleId1",
                table: "Exercise",
                column: "TargetMuscleId1",
                principalTable: "targetMuscles",
                principalColumn: "Id");
        }
    }
}
