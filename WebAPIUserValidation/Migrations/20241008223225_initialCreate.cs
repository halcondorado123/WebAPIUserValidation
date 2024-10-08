using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UVA");

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
                name: "ClientME",
                schema: "UVA",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    IdentificationId = table.Column<int>(type: "int", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    RelatId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientME", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientME_GenreME_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "UVA",
                        principalTable: "GenreME",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK_ClientME_IdentificationME_IdentificationId",
                        column: x => x.IdentificationId,
                        principalSchema: "UVA",
                        principalTable: "IdentificationME",
                        principalColumn: "IdentificationId");
                    table.ForeignKey(
                        name: "FK_ClientME_RelationShME_RelatId",
                        column: x => x.RelatId,
                        principalSchema: "UVA",
                        principalTable: "RelationShME",
                        principalColumn: "RelatId");
                    table.ForeignKey(
                        name: "FK_ClientME_RoleME_RolId",
                        column: x => x.RolId,
                        principalSchema: "UVA",
                        principalTable: "RoleME",
                        principalColumn: "RolID");
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                schema: "UVA",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_UserInfo_ClientME_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "UVA",
                        principalTable: "ClientME",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_GenreId",
                schema: "UVA",
                table: "ClientME",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_IdentificationId",
                schema: "UVA",
                table: "ClientME",
                column: "IdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_RelatId",
                schema: "UVA",
                table: "ClientME",
                column: "RelatId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_RolId",
                schema: "UVA",
                table: "ClientME",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "ClientME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "GenreME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "IdentificationME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "RelationShME",
                schema: "UVA");

            migrationBuilder.DropTable(
                name: "RoleME",
                schema: "UVA");
        }
    }
}
