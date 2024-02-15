using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQualificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Qualifications",
                newName: "Stream");

            migrationBuilder.RenameColumn(
                name: "GPA",
                table: "Qualifications",
                newName: "Percentage");

            migrationBuilder.AddColumn<string>(
                name: "QualificationName",
                table: "Qualifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationName",
                table: "Qualifications");

            migrationBuilder.RenameColumn(
                name: "Stream",
                table: "Qualifications",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Qualifications",
                newName: "GPA");
        }
    }
}
