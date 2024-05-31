using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergySolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmpTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employee");
        }
    }
}
