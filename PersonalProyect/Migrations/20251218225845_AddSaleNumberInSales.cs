using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProyect.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleNumberInSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<long>(
                name: "SaleNumberSequence",
                startValue: 1L,
                incrementBy: 1);

            migrationBuilder.AddColumn<long>(
                name: "SaleNumber",
                table: "Sales",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR SaleNumberSequence");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleNumber",
                table: "Sales");

            migrationBuilder.DropSequence(
                name: "SaleNumberSequence");
        }

    }
}
