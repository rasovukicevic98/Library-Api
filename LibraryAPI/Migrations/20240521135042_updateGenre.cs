using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06800c5e-7def-441b-b6f5-c1dc8d7204f9", "AQAAAAIAAYagAAAAEOOkC1Afh4QEwVb8VV+6wHZKLee2lFTbyoFuzWzR2EQdTb7SrwYn+8KoUSBYNT5Rig==", "93182ea0-90bf-4f99-ad82-7b418dde3166" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b5bf90c-fc5c-4cb8-b895-d5bcc994fca8", "AQAAAAIAAYagAAAAEEoSbssiAkesNukmFBFPBbwBi6aaGzC4u7D7uWivTdaysGyrlOlyOrfi6yKo24sQ/A==", "b172b9a7-4668-49d8-a7eb-0c63ce574f14" });
        }
    }
}
