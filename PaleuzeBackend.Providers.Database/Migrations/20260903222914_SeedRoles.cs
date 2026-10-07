using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaleuzeBackend.Providers.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("46db37fb-1894-4122-a782-2d75aafc1bd1"), "Admin", "Admin", "ADMIN" },
                    { new Guid("68f33d13-2d34-40ca-a3cd-460c4fc5e7a8"), "TournamentManager", "TournamentManager", "TOURNAMENTMANAGER" },
                    { new Guid("dcff469b-c71a-4bb1-b7b7-8352d2c23b14"), "TournamentViewer", "TournamentViewer", "TOURNAMENTVIEWER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("46db37fb-1894-4122-a782-2d75aafc1bd1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68f33d13-2d34-40ca-a3cd-460c4fc5e7a8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcff469b-c71a-4bb1-b7b7-8352d2c23b14"));
        }
    }
}
