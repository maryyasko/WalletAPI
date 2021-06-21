using Microsoft.EntityFrameworkCore.Migrations;

namespace WalletAPI.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wallets_WalletId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Users_WalletId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Rate = table.Column<double>(type: "REAL", nullable: false),
                    WalletId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_Currencies_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletId",
                table: "Users",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_WalletId",
                table: "Currencies",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wallets_WalletId",
                table: "Users",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
