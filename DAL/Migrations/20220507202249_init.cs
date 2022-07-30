using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "CashDatas",
                columns: table => new
                {
                    CashDataID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Invested = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDatas", x => x.CashDataID);
                    table.ForeignKey(
                        name: "FK_CashDatas_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryPortfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPortfolios", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_HistoryPortfolios_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_Portfolios_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingPortfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingPortfolios", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_TrackingPortfolios_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryInstuments",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitLose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFromMainPortfolio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryInstuments", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_HistoryInstuments_HistoryPortfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "HistoryPortfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioInstruments",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioInstruments", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_PortfolioInstruments_Portfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingInstruments",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingInstruments", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_TrackingInstruments_TrackingPortfolios_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "TrackingPortfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingInstumentPrice",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrackingInstrumentInstrumentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingInstumentPrice", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_TrackingInstumentPrice_TrackingInstruments_TrackingInstrumentInstrumentID",
                        column: x => x.TrackingInstrumentInstrumentID,
                        principalTable: "TrackingInstruments",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashDatas_UserID",
                table: "CashDatas",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryInstuments_PortfolioID",
                table: "HistoryInstuments",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPortfolios_UserID",
                table: "HistoryPortfolios",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioInstruments_PortfolioID",
                table: "PortfolioInstruments",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserID",
                table: "Portfolios",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingInstruments_PortfolioID",
                table: "TrackingInstruments",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingInstumentPrice_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentPrice",
                column: "TrackingInstrumentInstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingPortfolios_UserID",
                table: "TrackingPortfolios",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDatas");

            migrationBuilder.DropTable(
                name: "HistoryInstuments");

            migrationBuilder.DropTable(
                name: "PortfolioInstruments");

            migrationBuilder.DropTable(
                name: "TrackingInstumentPrice");

            migrationBuilder.DropTable(
                name: "HistoryPortfolios");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "TrackingInstruments");

            migrationBuilder.DropTable(
                name: "TrackingPortfolios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
