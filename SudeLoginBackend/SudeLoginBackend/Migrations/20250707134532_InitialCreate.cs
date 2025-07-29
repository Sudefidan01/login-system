using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudeLoginBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciRol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
