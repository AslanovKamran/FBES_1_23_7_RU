using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_EShop.Migrations
{
    /// <inheritdoc />
    public partial class TypoFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qauntity",
                table: "OrderProducts",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProducts",
                newName: "Qauntity");
        }
    }
}
