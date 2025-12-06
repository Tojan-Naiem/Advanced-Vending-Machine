using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachine.DAL.Migrations
{
    /// <inheritdoc />
    public partial class paymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Transactions",
                newName: "PaymentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Transactions",
                newName: "PaymentMethod");
        }
    }
}
