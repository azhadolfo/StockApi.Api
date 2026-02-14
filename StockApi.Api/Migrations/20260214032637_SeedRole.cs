using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockApi.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "18588f65-97f3-4a41-b73f-3d5a6c7fa0d0", "bbe16d99-2a17-463e-be2c-2d41769a5131", "Admin", "ADMIN" },
                    { "b99c2763-8b22-4cbe-b3d7-88aa10190f86", "554bcbf6-be81-4aa2-b174-adfdad43e9e5", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "18588f65-97f3-4a41-b73f-3d5a6c7fa0d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "b99c2763-8b22-4cbe-b3d7-88aa10190f86");
        }
    }
}
