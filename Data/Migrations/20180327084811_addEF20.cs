using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fresenius.Data.Migrations
{
    public partial class addEF20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSpareparts_Invoices_InvoiceId",
                table: "InvoiceSpareparts");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSpareparts_Spareparts_SparepartId",
                table: "InvoiceSpareparts");

            migrationBuilder.DropForeignKey(
                name: "FK_Spareparts_Countries_CountryId",
                table: "Spareparts");

            migrationBuilder.DropForeignKey(
                name: "FK_Spareparts_Equipments_EquipmentId",
                table: "Spareparts");

            migrationBuilder.DropForeignKey(
                name: "FK_Spareparts_Manufacturers_ManufacturerId",
                table: "Spareparts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spareparts",
                table: "Spareparts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSpareparts",
                table: "InvoiceSpareparts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Spareparts",
                newName: "Sparepart");

            migrationBuilder.RenameTable(
                name: "InvoiceSpareparts",
                newName: "InvoiceSparepart");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Spareparts_ManufacturerId",
                table: "Sparepart",
                newName: "IX_Sparepart_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Spareparts_EquipmentId",
                table: "Sparepart",
                newName: "IX_Sparepart_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Spareparts_CountryId",
                table: "Sparepart",
                newName: "IX_Sparepart_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceSpareparts_SparepartId",
                table: "InvoiceSparepart",
                newName: "IX_InvoiceSparepart_SparepartId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InvoiceSparepart",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sparepart",
                table: "Sparepart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSparepart",
                table: "InvoiceSparepart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceSparepart_InvoiceId",
                table: "InvoiceSparepart",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSparepart_Invoice_InvoiceId",
                table: "InvoiceSparepart",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceSparepart_Sparepart_SparepartId",
                table: "InvoiceSparepart",
                column: "SparepartId",
                principalTable: "Sparepart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sparepart_Countries_CountryId",
                table: "Sparepart",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sparepart_Equipments_EquipmentId",
                table: "Sparepart",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sparepart_Manufacturers_ManufacturerId",
                table: "Sparepart",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSparepart_Invoice_InvoiceId",
                table: "InvoiceSparepart");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceSparepart_Sparepart_SparepartId",
                table: "InvoiceSparepart");

            migrationBuilder.DropForeignKey(
                name: "FK_Sparepart_Countries_CountryId",
                table: "Sparepart");

            migrationBuilder.DropForeignKey(
                name: "FK_Sparepart_Equipments_EquipmentId",
                table: "Sparepart");

            migrationBuilder.DropForeignKey(
                name: "FK_Sparepart_Manufacturers_ManufacturerId",
                table: "Sparepart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sparepart",
                table: "Sparepart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSparepart",
                table: "InvoiceSparepart");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceSparepart_InvoiceId",
                table: "InvoiceSparepart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvoiceSparepart");

            migrationBuilder.RenameTable(
                name: "Sparepart",
                newName: "Spareparts");

            migrationBuilder.RenameTable(
                name: "InvoiceSparepart",
                newName: "InvoiceSpareparts");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Sparepart_ManufacturerId",
                table: "Spareparts",
                newName: "IX_Spareparts_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sparepart_EquipmentId",
                table: "Spareparts",
                newName: "IX_Spareparts_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Sparepart_CountryId",
                table: "Spareparts",
                newName: "IX_Spareparts_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceSparepart_SparepartId",
                table: "InvoiceSpareparts",
                newName: "IX_InvoiceSpareparts_SparepartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spareparts",
                table: "Spareparts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSpareparts",
                table: "InvoiceSpareparts",
                columns: new[] { "InvoiceId", "SparepartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Spareparts_Countries_CountryId",
                table: "Spareparts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spareparts_Equipments_EquipmentId",
                table: "Spareparts",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spareparts_Manufacturers_ManufacturerId",
                table: "Spareparts",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
