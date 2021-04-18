using Microsoft.EntityFrameworkCore.Migrations;

namespace Currency.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConvertCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    USD = table.Column<decimal>(nullable: false),
                    UER = table.Column<decimal>(nullable: false),
                    UAH = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    EntityCurrencyId = table.Column<int>(nullable: true),
                    NumCode = table.Column<string>(nullable: true),
                    CharCode = table.Column<string>(nullable: true),
                    Nominal = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConvertCurrency_ConvertCurrency_EntityCurrencyId",
                        column: x => x.EntityCurrencyId,
                        principalTable: "ConvertCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConvertCurrency_EntityCurrencyId",
                table: "ConvertCurrency",
                column: "EntityCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvertCurrency");
        }
    }
}
