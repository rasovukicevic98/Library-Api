using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterBookRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRent_AspNetUsers_UserId",
                table: "BookRent");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRent_Books_BookId",
                table: "BookRent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRent",
                table: "BookRent");

            migrationBuilder.RenameTable(
                name: "BookRent",
                newName: "BookRents");

            migrationBuilder.RenameIndex(
                name: "IX_BookRent_BookId",
                table: "BookRents",
                newName: "IX_BookRents_BookId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookRents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRents",
                table: "BookRents",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "360d7bc2-f542-431f-86d4-5e14f9fdc2b8", "AQAAAAIAAYagAAAAECYxRyxl7wd0MsYZGVZz325+C0ttMxYNczEUV7gCkM2o8mFlaED07GCJxN/u2ZAHuw==", "6cf19e3f-ad4f-41bf-a6ed-b529ac5b1063" });

            migrationBuilder.CreateIndex(
                name: "IX_BookRents_UserId",
                table: "BookRents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRents_AspNetUsers_UserId",
                table: "BookRents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRents_Books_BookId",
                table: "BookRents",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRents_AspNetUsers_UserId",
                table: "BookRents");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRents_Books_BookId",
                table: "BookRents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRents",
                table: "BookRents");

            migrationBuilder.DropIndex(
                name: "IX_BookRents_UserId",
                table: "BookRents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookRents");

            migrationBuilder.RenameTable(
                name: "BookRents",
                newName: "BookRent");

            migrationBuilder.RenameIndex(
                name: "IX_BookRents_BookId",
                table: "BookRent",
                newName: "IX_BookRent_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRent",
                table: "BookRent",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "181fbe57-3ad4-4892-9009-4301e253b8f4", "AQAAAAIAAYagAAAAEO4BXU055lodBLH2zMaV8e6mkRaCRzNr7HhCnmjCCXOuIk4rLmRBwaxAmwIeQVDnHA==", "8845eaa6-0361-414e-9558-7432f3bf5725" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookRent_AspNetUsers_UserId",
                table: "BookRent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRent_Books_BookId",
                table: "BookRent",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
