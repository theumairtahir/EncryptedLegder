using Microsoft.EntityFrameworkCore.Migrations;

namespace EncryptedLedgerExample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactioneeId = table.Column<int>(nullable: false),
                    Debit = table.Column<string>(unicode: false, nullable: false),
                    Credit = table.Column<string>(unicode: false, nullable: false),
                    Balance = table.Column<string>(unicode: false, nullable: false),
                    TransactionDateTime = table.Column<string>(unicode: false, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    Comments = table.Column<string>(unicode: false, nullable: false),
                    Tag1 = table.Column<string>(unicode: false, nullable: true),
                    Tag2 = table.Column<string>(unicode: false, nullable: true),
                    Tag3 = table.Column<string>(unicode: false, nullable: true),
                    Signature = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
