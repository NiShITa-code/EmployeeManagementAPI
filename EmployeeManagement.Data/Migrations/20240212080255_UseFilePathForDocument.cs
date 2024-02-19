using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class UseFilePathForDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "EmployeeDocuments");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "EmployeeDocuments");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "EmployeeDocuments",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
