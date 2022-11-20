using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class changeKeyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingInstumentPrice_TrackingInstruments_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingInstumentPrice",
                table: "TrackingInstumentPrice");

            migrationBuilder.RenameTable(
                name: "TrackingInstumentPrice",
                newName: "TrackingInstumentsPrice");

            migrationBuilder.RenameColumn(
                name: "InstrumentID",
                table: "TrackingInstumentsPrice",
                newName: "InstrumentTrackingPriceID");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingInstumentPrice_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentsPrice",
                newName: "IX_TrackingInstumentsPrice_TrackingInstrumentInstrumentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingInstumentsPrice",
                table: "TrackingInstumentsPrice",
                column: "InstrumentTrackingPriceID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingInstumentsPrice_TrackingInstruments_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentsPrice",
                column: "TrackingInstrumentInstrumentID",
                principalTable: "TrackingInstruments",
                principalColumn: "InstrumentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingInstumentsPrice_TrackingInstruments_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentsPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingInstumentsPrice",
                table: "TrackingInstumentsPrice");

            migrationBuilder.RenameTable(
                name: "TrackingInstumentsPrice",
                newName: "TrackingInstumentPrice");

            migrationBuilder.RenameColumn(
                name: "InstrumentTrackingPriceID",
                table: "TrackingInstumentPrice",
                newName: "InstrumentID");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingInstumentsPrice_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentPrice",
                newName: "IX_TrackingInstumentPrice_TrackingInstrumentInstrumentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingInstumentPrice",
                table: "TrackingInstumentPrice",
                column: "InstrumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingInstumentPrice_TrackingInstruments_TrackingInstrumentInstrumentID",
                table: "TrackingInstumentPrice",
                column: "TrackingInstrumentInstrumentID",
                principalTable: "TrackingInstruments",
                principalColumn: "InstrumentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
