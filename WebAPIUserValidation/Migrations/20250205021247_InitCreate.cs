using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UVA");

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

            migrationBuilder.CreateTable(
                name: "IdentificationME",
                schema: "UVA",
                columns: table => new
                {
                    IdentificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationME", x => x.IdentificationId);
                });

            migrationBuilder.CreateTable(
                name: "RoleME",
                schema: "UVA",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleME", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "StatusME",
                schema: "UVA",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusME", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "PersonME",
                schema: "UVA",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationId = table.Column<int>(type: "int", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserInfoPersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonME", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonME_GenderME_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "UVA",
                        principalTable: "GenderME",
                        principalColumn: "GenderId");
                    table.ForeignKey(
                        name: "FK_PersonME_IdentificationME_IdentificationId",
                        column: x => x.IdentificationId,
                        principalSchema: "UVA",
                        principalTable: "IdentificationME",
                        principalColumn: "IdentificationId");
                });

            migrationBuilder.CreateTable(
                name: "UserME",
                schema: "UVA",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserPasswordHash = table.Column<string>(type: "varchar(200)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    RoleRolID = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserME", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_UserME_PersonME_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "UVA",
                        principalTable: "PersonME",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserME_RoleME_RoleRolID",
                        column: x => x.RoleRolID,
                        principalSchema: "UVA",
                        principalTable: "RoleME",
                        principalColumn: "RolId");
                    table.ForeignKey(
                        name: "FK_UserME_StatusME_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "UVA",
                        principalTable: "StatusME",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonME_GenderId",
                schema: "UVA",
                table: "PersonME",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonME_IdentificationId",
                schema: "UVA",
                table: "PersonME",
                column: "IdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonME_UserInfoPersonId",
                schema: "UVA",
                table: "PersonME",
                column: "UserInfoPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserME_RoleRolID",
                schema: "UVA",
                table: "UserME",
                column: "RoleRolID");

            migrationBuilder.CreateIndex(
                name: "IX_UserME_StatusId",
                schema: "UVA",
                table: "UserME",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonME_UserME_UserInfoPersonId",
                schema: "UVA",
                table: "PersonME",
                column: "UserInfoPersonId",
                principalSchema: "UVA",
                principalTable: "UserME",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonME_GenderME_GenderId",
                schema: "UVA",
                table: "PersonME");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonME_IdentificationME_IdentificationId",
                schema: "UVA",
                table: "PersonME");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonME_UserME_UserInfoPersonId",
                schema: "UVA",
                table: "PersonME");

            migrationBuilder.DropTable(
                name: "GenderME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "IdentificationME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "UserME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "PersonME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "RoleME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "StatusME",
                schema: "UVA");
        }
    }
}
