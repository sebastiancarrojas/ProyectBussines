using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProyect.Migrations
{
    /// <inheritdoc />
    public partial class FixSaleDetailForTemporaryProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "SalesDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsTemporary",
                table: "SalesDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SalesDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "IsTemporary",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SalesDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "SalesDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
