using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectAssessment.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomerstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tbCustomers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "tbCustomers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "CompanyName",
                value: "Retail Solutions");

            migrationBuilder.UpdateData(
                table: "tbCustomers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "CompanyName",
                value: "Finance Corp");

            migrationBuilder.UpdateData(
                table: "tbCustomers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "CompanyName",
                value: "Tech Solutions");

            migrationBuilder.UpdateData(
                table: "tbCustomers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "CompanyName",
                value: "Logistics Ltd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "tbCustomers");
        }
    }
}
