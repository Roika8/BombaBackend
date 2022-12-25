using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations.MainData
{
    public partial class initMainData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryPortfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPortfolios", x => x.PortfolioID);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioID);
                });

            migrationBuilder.CreateTable(
                name: "TrackingPortfolios",
                columns: table => new
                {
                    PortfolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingPortfolios", x => x.PortfolioID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryInstuments",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionOccuredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestOccured = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryInstuments", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_HistoryInstuments_HistoryPortfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "HistoryPortfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioInstruments",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StopLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TakeProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChartPattern = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioInstruments", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_PortfolioInstruments_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackingInstruments",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingInstruments", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_TrackingInstruments_TrackingPortfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "TrackingPortfolios",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashDatas",
                columns: table => new
                {
                    CashDataID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Invested = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDatas", x => x.CashDataID);
                    table.ForeignKey(
                        name: "FK_CashDatas_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingInstumentsPrice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingInstumentsPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrackingInstumentsPrice_TrackingInstruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "TrackingInstruments",
                        principalColumn: "InstrumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashDatas_UserID",
                table: "CashDatas",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryInstuments_PortfolioId",
                table: "HistoryInstuments",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioInstruments_PortfolioId",
                table: "PortfolioInstruments",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingInstruments_PortfolioId",
                table: "TrackingInstruments",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingInstumentsPrice_InstrumentID",
                table: "TrackingInstumentsPrice",
                column: "InstrumentID");
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
                name: "TrackingInstumentsPrice");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "HistoryPortfolios");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "TrackingInstruments");

            migrationBuilder.DropTable(
                name: "TrackingPortfolios");
        }
    }
}
