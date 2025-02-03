using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Api.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "TrainersData");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Packages",
                newName: "OldPrice");

            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "Packages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                table: "Packages",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "TrainersData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
