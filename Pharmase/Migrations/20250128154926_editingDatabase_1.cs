using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmase.Migrations
{
    /// <inheritdoc />
    public partial class editingDatabase_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Medicines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
