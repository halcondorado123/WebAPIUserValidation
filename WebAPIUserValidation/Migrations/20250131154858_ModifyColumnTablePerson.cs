using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class ModifyColumnTablePerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientME_PersonME_ClientId",
                schema: "UVA",
                table: "ClientME");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "UVA",
                table: "PersonME",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "UVA",
                table: "ClientME",
                newName: "PersonId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "UVA",
                table: "PersonME",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientME_PersonME_PersonId",
                schema: "UVA",
                table: "ClientME",
                column: "PersonId",
                principalSchema: "UVA",
                principalTable: "PersonME",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientME_PersonME_PersonId",
                schema: "UVA",
                table: "ClientME");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "UVA",
                table: "PersonME",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "UVA",
                table: "ClientME",
                newName: "ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "UVA",
                table: "PersonME",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientME_PersonME_ClientId",
                schema: "UVA",
                table: "ClientME",
                column: "ClientId",
                principalSchema: "UVA",
                principalTable: "PersonME",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
