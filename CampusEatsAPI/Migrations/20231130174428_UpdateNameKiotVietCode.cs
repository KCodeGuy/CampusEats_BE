using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameKiotVietCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "KiotVietOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KiotVietOrderId",
                table: "Orders",
                newName: "OrderId");
        }
    }
}
