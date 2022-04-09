using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace haris_edin_rs1.Migrations
{
    public partial class dautmobjave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pitanje_Artikal_Artikal_id",
                table: "pitanje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pitanje",
                table: "pitanje");

            migrationBuilder.DropColumn(
                name: "Popust",
                table: "Artikal");

            migrationBuilder.RenameTable(
                name: "pitanje",
                newName: "Pitanje");

            migrationBuilder.RenameIndex(
                name: "IX_pitanje_Artikal_id",
                table: "Pitanje",
                newName: "IX_Pitanje_Artikal_id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumObjave",
                table: "Artikal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pitanje",
                table: "Pitanje",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pitanje_Artikal_Artikal_id",
                table: "Pitanje",
                column: "Artikal_id",
                principalTable: "Artikal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pitanje_Artikal_Artikal_id",
                table: "Pitanje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pitanje",
                table: "Pitanje");

            migrationBuilder.DropColumn(
                name: "DatumObjave",
                table: "Artikal");

            migrationBuilder.RenameTable(
                name: "Pitanje",
                newName: "pitanje");

            migrationBuilder.RenameIndex(
                name: "IX_Pitanje_Artikal_id",
                table: "pitanje",
                newName: "IX_pitanje_Artikal_id");

            migrationBuilder.AddColumn<int>(
                name: "Popust",
                table: "Artikal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pitanje",
                table: "pitanje",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pitanje_Artikal_Artikal_id",
                table: "pitanje",
                column: "Artikal_id",
                principalTable: "Artikal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
