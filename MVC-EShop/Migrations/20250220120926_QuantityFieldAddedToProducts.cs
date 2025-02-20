using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_EShop.Migrations
{
    /// <inheritdoc />
    public partial class QuantityFieldAddedToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qauntity",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qauntity",
                table: "OrderProducts");
        }
    }
}
