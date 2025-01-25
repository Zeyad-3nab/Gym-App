using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Api.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodAndExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ExerciseSystems_ExerciseSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FoodSystems_FoodSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExerciseSystems");

            migrationBuilder.DropTable(
                name: "FoodSystems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExerciseSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FoodSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExerciseSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FoodSystemId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndPackage",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartPackage",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfCalories = table.Column<double>(type: "float", nullable: false),
                    NumOfProtein = table.Column<double>(type: "float", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "applicationUserFoods",
                columns: table => new
                {
                    applicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    foodId = table.Column<int>(type: "int", nullable: false),
                    NumOfGrams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationUserFoods", x => new { x.foodId, x.applicationUserId });
                    table.ForeignKey(
                        name: "FK_applicationUserFoods_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_applicationUserFoods_Food_foodId",
                        column: x => x.foodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetMuscleId = table.Column<int>(type: "int", nullable: false),
                    TargetMuscleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_targetMuscles_TargetMuscleId",
                        column: x => x.TargetMuscleId,
                        principalTable: "targetMuscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_targetMuscles_TargetMuscleId1",
                        column: x => x.TargetMuscleId1,
                        principalTable: "targetMuscles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "applicationUserExercises",
                columns: table => new
                {
                    applicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    exerciseId = table.Column<int>(type: "int", nullable: false),
                    NumOfGroups = table.Column<int>(type: "int", nullable: false),
                    NumOfCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationUserExercises", x => new { x.exerciseId, x.applicationUserId });
                    table.ForeignKey(
                        name: "FK_applicationUserExercises_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_applicationUserExercises_Exercise_exerciseId",
                        column: x => x.exerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationUserExercises_applicationUserId",
                table: "applicationUserExercises",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_applicationUserFoods_applicationUserId",
                table: "applicationUserFoods",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TargetMuscleId",
                table: "Exercise",
                column: "TargetMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TargetMuscleId1",
                table: "Exercise",
                column: "TargetMuscleId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationUserExercises");

            migrationBuilder.DropTable(
                name: "applicationUserFoods");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "targetMuscles");

            migrationBuilder.DropColumn(
                name: "EndPackage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StartPackage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseSystemId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodSystemId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExerciseSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetMuscle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfCalories = table.Column<double>(type: "float", nullable: false),
                    NumberOfProteins = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodSystems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExerciseSystemId",
                table: "AspNetUsers",
                column: "ExerciseSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FoodSystemId",
                table: "AspNetUsers",
                column: "FoodSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ExerciseSystems_ExerciseSystemId",
                table: "AspNetUsers",
                column: "ExerciseSystemId",
                principalTable: "ExerciseSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FoodSystems_FoodSystemId",
                table: "AspNetUsers",
                column: "FoodSystemId",
                principalTable: "FoodSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
