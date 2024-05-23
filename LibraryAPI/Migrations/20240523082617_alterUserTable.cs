using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class alterUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "809f727e-cf6b-4344-8e41-73d59fcee184", null, null, "AQAAAAIAAYagAAAAEACEcH5Ta1IPiCxD4kf1+Nh+zgE6ioSfNf0/bJmxy8rGh6ducZqOXyyO9MQKg53tBA==", "ebe44da3-6ff3-48e9-a73a-3c60814aecaf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06800c5e-7def-441b-b6f5-c1dc8d7204f9", "AQAAAAIAAYagAAAAEOOkC1Afh4QEwVb8VV+6wHZKLee2lFTbyoFuzWzR2EQdTb7SrwYn+8KoUSBYNT5Rig==", "93182ea0-90bf-4f99-ad82-7b418dde3166" });
        }
    }
}
