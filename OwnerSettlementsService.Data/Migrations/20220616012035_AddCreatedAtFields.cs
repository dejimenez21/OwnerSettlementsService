using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnerSettlementsService.Data.Migrations
{
    public partial class AddCreatedAtFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterAt",
                table: "OwnerContracts",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "OwnerContracts",
                newName: "RegisterAt");
        }
    }
}
