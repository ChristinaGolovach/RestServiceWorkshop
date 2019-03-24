using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGamesServiceApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGame",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGame", x => x.GameID);
                });

            migrationBuilder.InsertData(
                table: "BoardGame",
                columns: new[] { "GameID", "Description", "Price" },
                values: new object[] { 1, "Test game 1", 11m });

            migrationBuilder.InsertData(
                table: "BoardGame",
                columns: new[] { "GameID", "Description", "Price" },
                values: new object[] { 2, "Test game 2", 12m });

            migrationBuilder.InsertData(
                table: "BoardGame",
                columns: new[] { "GameID", "Description", "Price" },
                values: new object[] { 3, "Test game 3", 13m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGame");
        }
    }
}
