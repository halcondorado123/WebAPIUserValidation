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
                name: "RelationShME",
                schema: "UVA",
                columns: table => new
                {
                    RelatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationShME", x => x.RelatId);
                });

            migrationBuilder.CreateTable(
                name: "RoleME",
                schema: "UVA",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleME", x => x.RolID);
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
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationId = table.Column<int>(type: "int", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonME", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_PersonME_GenderME_Gender",
                        column: x => x.Gender,
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
                name: "ClientME",
                schema: "UVA",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    UsuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientME", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientME_PersonME_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "UVA",
                        principalTable: "PersonME",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientME_RoleME_RolId",
                        column: x => x.RolId,
                        principalSchema: "UVA",
                        principalTable: "RoleME",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                schema: "UVA",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserPasswordHash = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserInfo_PersonME_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "UVA",
                        principalTable: "PersonME",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_RolId",
                schema: "UVA",
                table: "ClientME",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonME_Gender",
                schema: "UVA",
                table: "PersonME",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_PersonME_IdentificationId",
                schema: "UVA",
                table: "PersonME",
                column: "IdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_PersonId",
                schema: "UVA",
                table: "UserInfo",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "RelationShME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "StatusME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "UserInfo",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "RoleME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "PersonME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "GenderME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "IdentificationME",
                schema: "UVA");
        }
    }
}
