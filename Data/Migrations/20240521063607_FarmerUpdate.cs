using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergySolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class FarmerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Farmer",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Farmer",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
               name: "Name",
               table: "Farmer",
               newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Farmer",
                newName: "Password");
        }
    }
}
