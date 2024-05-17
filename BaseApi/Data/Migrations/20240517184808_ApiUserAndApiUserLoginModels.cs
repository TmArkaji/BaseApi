using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiUserAndApiUserLoginModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUsers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryA2 = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApiUserLogins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUserLogins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApiUserLogins_ApiUsers_ApiUserId",
                        column: x => x.ApiUserId,
                        principalTable: "ApiUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUserLogins_ApiUserId",
                table: "ApiUserLogins",
                column: "ApiUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUserLogins");

            migrationBuilder.DropTable(
                name: "ApiUsers");
        }
    }
}
