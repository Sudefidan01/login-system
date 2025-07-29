using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudeLoginBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTarihVeAktiflik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "Kullanicilar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarihi",
                table: "Kullanicilar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OlusturmaTarihi",
                table: "Kullanicilar",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "GuncellenmeTarihi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "OlusturmaTarihi",
                table: "Kullanicilar");
        }
    }
}
