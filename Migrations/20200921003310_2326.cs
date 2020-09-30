using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CP.API.Migrations
{
    public partial class _2326 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CoupanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoupanTitle = table.Column<string>(nullable: true),
                    CouponCode = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MinimumSpend = table.Column<int>(nullable: false),
                    MaximumSpend = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CoupanId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CouponId",
                table: "Products",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CoupanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Coupons_CouponId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Products_CouponId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CouponCode = table.Column<string>(nullable: true),
                    DiscountName = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });
        }
    }
}
