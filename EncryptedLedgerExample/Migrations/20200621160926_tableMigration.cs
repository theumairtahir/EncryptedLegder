using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncryptedLedgerExample.Migrations
{
    public partial class tableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDateTime",
                table: "Table",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionDateTime",
                table: "Table",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
