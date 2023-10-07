using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dme.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameEntity_TitleEntity_TitleId",
                        column: x => x.TitleId,
                        principalTable: "TitleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RegisteredAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_NameEntity_NameId",
                        column: x => x.NameId,
                        principalTable: "NameEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentEntity_DocumentTypeEntity_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PictureEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Large = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntity_DocumentTypeId",
                table: "DocumentEntity",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEntity_UserId",
                table: "DocumentEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NameEntity_TitleId",
                table: "NameEntity",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureEntity_UserId",
                table: "PictureEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NameId",
                table: "Users",
                column: "NameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentEntity");

            migrationBuilder.DropTable(
                name: "PictureEntity");

            migrationBuilder.DropTable(
                name: "DocumentTypeEntity");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NameEntity");

            migrationBuilder.DropTable(
                name: "TitleEntity");
        }
    }
}
