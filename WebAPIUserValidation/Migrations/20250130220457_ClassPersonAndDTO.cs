using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class ClassPersonAndDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "UVA",
                table: "PersonME",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "UVA",
                table: "PersonME",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "UVA",
                table: "PersonME");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "UVA",
                table: "PersonME");
        }
    }
}
