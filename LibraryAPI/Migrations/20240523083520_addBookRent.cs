using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class addBookRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookRent",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRent", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookRent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRent_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "181fbe57-3ad4-4892-9009-4301e253b8f4", "AQAAAAIAAYagAAAAEO4BXU055lodBLH2zMaV8e6mkRaCRzNr7HhCnmjCCXOuIk4rLmRBwaxAmwIeQVDnHA==", "8845eaa6-0361-414e-9558-7432f3bf5725" });

            migrationBuilder.CreateIndex(
                name: "IX_BookRent_BookId",
                table: "BookRent",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRent");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "809f727e-cf6b-4344-8e41-73d59fcee184", "AQAAAAIAAYagAAAAEACEcH5Ta1IPiCxD4kf1+Nh+zgE6ioSfNf0/bJmxy8rGh6ducZqOXyyO9MQKg53tBA==", "ebe44da3-6ff3-48e9-a73a-3c60814aecaf" });
        }
    }
}
