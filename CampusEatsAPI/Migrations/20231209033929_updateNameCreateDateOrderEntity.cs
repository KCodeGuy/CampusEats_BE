using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusEatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateNameCreateDateOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "Orders",
                newName: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Orders",
                newName: "CreateData");
        }
    }
}
