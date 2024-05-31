using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergySolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Employee",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                   email = table.Column<string>(maxLength: 256, nullable: true),
                   PasswordHash = table.Column<string>(nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Employee", x => x.Id);
               });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
              name: "Employee");
        }
    }
}
