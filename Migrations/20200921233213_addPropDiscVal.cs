using Microsoft.EntityFrameworkCore.Migrations;

namespace CP.API.Migrations
{
    public partial class addPropDiscVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountValue",
                table: "Coupons",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "Coupons");
        }
    }
}
