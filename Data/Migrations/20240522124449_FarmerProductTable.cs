using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergySolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class FarmerProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "FarmerProducts",
               columns: table => new
               {
                   FarmerId = table.Column<int>(nullable: false),
                   ProductId = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_FarmerProducts", x => new { x.FarmerId, x.ProductId });
                   table.ForeignKey(
                       name: "FK_FarmerProducts_Farmer_FarmerId",
                       column: x => x.FarmerId,
                       principalTable: "Farmer",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_FarmerProducts_Products_ProductId",
                       column: x => x.ProductId,
                       principalTable: "Products",
                       principalColumn: "ProductId",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_FarmerProducts_ProductId",
                table: "FarmerProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmerProducts");
        }
    }
}
