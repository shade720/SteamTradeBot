using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SteamTradeBotService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EngItemName = table.Column<string>(type: "text", nullable: true),
                    RusItemName = table.Column<string>(type: "text", nullable: true),
                    BuyPrice = table.Column<double>(type: "double precision", nullable: false),
                    SellPrice = table.Column<double>(type: "double precision", nullable: false),
                    AvgPrice = table.Column<double>(type: "double precision", nullable: false),
                    Trend = table.Column<double>(type: "double precision", nullable: false),
                    Sales = table.Column<double>(type: "double precision", nullable: false),
                    IsTherePurchaseOrder = table.Column<bool>(type: "boolean", nullable: false),
                    BuyOrderQuantity = table.Column<int>(type: "integer", nullable: false),
                    ItemPriority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
