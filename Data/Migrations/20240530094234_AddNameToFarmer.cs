using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergySolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToFarmer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Farmer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Farmer");
        }
    }
}
