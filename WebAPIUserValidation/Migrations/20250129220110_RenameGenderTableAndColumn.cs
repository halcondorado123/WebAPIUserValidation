using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class RenameGenderTableAndColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientME_GenreME_GenreId",
                schema: "UVA",
                table: "ClientME");

            migrationBuilder.DropTable(
                name: "GenreME",
                schema: "UVA");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                schema: "UVA",
                table: "ClientME",
                newName: "Gender");

            migrationBuilder.RenameIndex(
                name: "IX_ClientME_GenreId",
                schema: "UVA",
                table: "ClientME",
                newName: "IX_ClientME_Gender");

            migrationBuilder.CreateTable(
                name: "GenderME",
                schema: "UVA",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderME", x => x.GenderId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientME_GenderME_Gender",
                schema: "UVA",
                table: "ClientME",
                column: "Gender",
                principalSchema: "UVA",
                principalTable: "GenderME",
                principalColumn: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientME_GenderME_Gender",
                schema: "UVA",
                table: "ClientME");

            migrationBuilder.DropTable(
                name: "GenderME",
                schema: "UVA");

            migrationBuilder.RenameColumn(
                name: "Gender",
                schema: "UVA",
                table: "ClientME",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientME_Gender",
                schema: "UVA",
                table: "ClientME",
                newName: "IX_ClientME_GenreId");

            migrationBuilder.CreateTable(
                name: "GenreME",
                schema: "UVA",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreME", x => x.GenreId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientME_GenreME_GenreId",
                schema: "UVA",
                table: "ClientME",
                column: "GenreId",
                principalSchema: "UVA",
                principalTable: "GenreME",
                principalColumn: "GenreId");
        }
    }
}
