using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachine.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addChagnes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MachineStateLogs_Id",
                table: "MachineStateLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MachineStateLogs_Timestamp",
                table: "MachineStateLogs",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MachineStateLogs_Id",
                table: "MachineStateLogs");

            migrationBuilder.DropIndex(
                name: "IX_MachineStateLogs_Timestamp",
                table: "MachineStateLogs");
        }
    }
}
