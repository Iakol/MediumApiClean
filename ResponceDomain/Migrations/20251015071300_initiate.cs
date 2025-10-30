using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResponceDomain.Migrations
{
    /// <inheritdoc />
    public partial class initiate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responces",
                columns: table => new
                {
                    ResponceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextOfReply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReaderTypeEnum = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseResponseId = table.Column<int>(type: "int", nullable: true),
                    BaseResponceResponceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responces", x => x.ResponceId);
                    table.ForeignKey(
                        name: "FK_Responces_Responces_BaseResponceResponceId",
                        column: x => x.BaseResponceResponceId,
                        principalTable: "Responces",
                        principalColumn: "ResponceId");
                });

            migrationBuilder.CreateTable(
                name: "Claps",
                columns: table => new
                {
                    ResponceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClapsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claps", x => new { x.UserId, x.ResponceId });
                    table.ForeignKey(
                        name: "FK_Claps_Responces_ResponceId",
                        column: x => x.ResponceId,
                        principalTable: "Responces",
                        principalColumn: "ResponceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claps_ResponceId",
                table: "Claps",
                column: "ResponceId");

            migrationBuilder.CreateIndex(
                name: "IX_Responces_BaseResponceResponceId",
                table: "Responces",
                column: "BaseResponceResponceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claps");

            migrationBuilder.DropTable(
                name: "Responces");
        }
    }
}
