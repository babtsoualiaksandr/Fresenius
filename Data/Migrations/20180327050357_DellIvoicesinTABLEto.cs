using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fresenius.Data.Migrations
{
    public partial class DellIvoicesinTABLEto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spareparts_Invoices_InvoiceId",
                table: "Spareparts");

            migrationBuilder.DropIndex(
                name: "IX_Spareparts_InvoiceId",
                table: "Spareparts");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Spareparts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Spareparts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spareparts_InvoiceId",
                table: "Spareparts",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spareparts_Invoices_InvoiceId",
                table: "Spareparts",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
