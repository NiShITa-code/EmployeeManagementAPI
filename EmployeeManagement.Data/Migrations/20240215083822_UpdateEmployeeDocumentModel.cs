using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "EmployeeDocuments");
        }
    }
}
