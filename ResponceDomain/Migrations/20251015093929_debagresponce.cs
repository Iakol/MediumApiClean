using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResponceDomain.Migrations
{
    /// <inheritdoc />
    public partial class debagresponce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responces_Responces_BaseResponceResponceId",
                table: "Responces");

            migrationBuilder.DropIndex(
                name: "IX_Responces_BaseResponceResponceId",
                table: "Responces");

            migrationBuilder.DropColumn(
                name: "BaseResponceResponceId",
                table: "Responces");

            migrationBuilder.CreateIndex(
                name: "IX_Responces_BaseResponseId",
                table: "Responces",
                column: "BaseResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responces_Responces_BaseResponseId",
                table: "Responces",
                column: "BaseResponseId",
                principalTable: "Responces",
                principalColumn: "ResponceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responces_Responces_BaseResponseId",
                table: "Responces");

            migrationBuilder.DropIndex(
                name: "IX_Responces_BaseResponseId",
                table: "Responces");

            migrationBuilder.AddColumn<int>(
                name: "BaseResponceResponceId",
                table: "Responces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responces_BaseResponceResponceId",
                table: "Responces",
                column: "BaseResponceResponceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responces_Responces_BaseResponceResponceId",
                table: "Responces",
                column: "BaseResponceResponceId",
                principalTable: "Responces",
                principalColumn: "ResponceId");
        }
    }
}
