using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNewColumnAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Account",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Account");
        }
    }
}
