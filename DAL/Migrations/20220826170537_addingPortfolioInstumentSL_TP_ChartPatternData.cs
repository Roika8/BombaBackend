using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addingPortfolioInstumentSL_TP_ChartPatternData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_UserID",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_UserID",
                table: "Portfolios");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Portfolios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChartPattern",
                table: "PortfolioInstruments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "StopLoss",
                table: "PortfolioInstruments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TakeProfit",
                table: "PortfolioInstruments",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChartPattern",
                table: "PortfolioInstruments");

            migrationBuilder.DropColumn(
                name: "StopLoss",
                table: "PortfolioInstruments");

            migrationBuilder.DropColumn(
                name: "TakeProfit",
                table: "PortfolioInstruments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Portfolios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserID",
                table: "Portfolios",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_UserID",
                table: "Portfolios",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
