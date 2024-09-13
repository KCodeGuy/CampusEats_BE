using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNewColumnOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategogyName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategogyName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "OrderDetails");
        }
    }
}
