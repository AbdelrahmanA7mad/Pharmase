using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmase.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Medicines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
