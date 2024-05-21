using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class addBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c7bf418-c96a-4497-971e-6645449300a9", "AQAAAAIAAYagAAAAEMr9PWlaPCH5bByEAbDIO3GsyyQPnZsaFK13m8Tt6owTSrl8VLiIedwE4CPg5zUVHg==", "1612ab9d-fb42-40d5-98b5-762f2b5145e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5da96b07-1dd2-46d0-b457-bcc287d801b9", "AQAAAAIAAYagAAAAEBRaZIqdvsN00ia6WHew4yJWw0hDgCf2YoH3wYunx0/mBd0nBDoGllNu7Fvb80yAcw==", "1a8156d4-c110-48fc-9349-9b9970f51434" });
        }
    }
}
