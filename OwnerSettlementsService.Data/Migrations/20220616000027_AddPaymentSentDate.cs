using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnerSettlementsService.Data.Migrations
{
    public partial class AddPaymentSentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Delivery",
                table: "Payments",
                newName: "DeliveredBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "DeliveredBy",
                table: "Payments",
                newName: "Delivery");
        }
    }
}
