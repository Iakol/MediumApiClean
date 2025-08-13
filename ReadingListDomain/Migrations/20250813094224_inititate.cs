using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingListDomain.Migrations
{
    /// <inheritdoc />
    public partial class inititate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadingLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadingListCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Immortal = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    IsOpenResponces = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoryInReadingListModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryInReadingListModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryInReadingListModels_ReadingLists_ReadingListId",
                        column: x => x.ReadingListId,
                        principalTable: "ReadingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryInReadingListModels_ReadingListId",
                table: "StoryInReadingListModels",
                column: "ReadingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryInReadingListModels");

            migrationBuilder.DropTable(
                name: "ReadingLists");
        }
    }
}
