using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnerSettlementsService.Data.Migrations
{
    public partial class RemoveToOwnerFieldFromPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToOwner",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ToOwner",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
