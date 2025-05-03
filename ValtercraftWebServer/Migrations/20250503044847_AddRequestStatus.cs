using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValtercraftWebServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WhiteListRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WhiteListRequests");
        }
    }
}
