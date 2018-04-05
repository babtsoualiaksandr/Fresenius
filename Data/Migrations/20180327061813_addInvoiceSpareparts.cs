using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fresenius.Data.Migrations
{
    public partial class addInvoiceSpareparts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSparepart_Invoices_InvoiceId",
                table: "InvoiceSparepart");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSparepart_Spareparts_SparepartId",
                table: "InvoiceSparepart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSparepart",
                table: "InvoiceSparepart");

            migrationBuilder.RenameTable(
                name: "InvoiceSparepart",
                newName: "InvoiceSpareparts");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceSparepart_SparepartId",
                table: "InvoiceSpareparts",
                newName: "IX_InvoiceSpareparts_SparepartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSpareparts",
                table: "InvoiceSpareparts",
                columns: new[] { "InvoiceId", "SparepartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSpareparts_Invoices_InvoiceId",
                table: "InvoiceSpareparts",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSpareparts_Spareparts_SparepartId",
                table: "InvoiceSpareparts",
                column: "SparepartId",
                principalTable: "Spareparts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSpareparts_Invoices_InvoiceId",
                table: "InvoiceSpareparts");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSpareparts_Spareparts_SparepartId",
                table: "InvoiceSpareparts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSpareparts",
                table: "InvoiceSpareparts");

            migrationBuilder.RenameTable(
                name: "InvoiceSpareparts",
                newName: "InvoiceSparepart");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceSpareparts_SparepartId",
                table: "InvoiceSparepart",
                newName: "IX_InvoiceSparepart_SparepartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSparepart",
                table: "InvoiceSparepart",
                columns: new[] { "InvoiceId", "SparepartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSparepart_Invoices_InvoiceId",
                table: "InvoiceSparepart",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSparepart_Spareparts_SparepartId",
                table: "InvoiceSparepart",
                column: "SparepartId",
                principalTable: "Spareparts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
