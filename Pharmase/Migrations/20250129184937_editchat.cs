using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmase.Migrations
{
    /// <inheritdoc />
    public partial class editchat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "SaleItems");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Sales",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "SaleItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SaleItems",
                newName: "QuantitySold");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "QuantitySold",
                table: "SaleItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SaleItems",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "SaleItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
