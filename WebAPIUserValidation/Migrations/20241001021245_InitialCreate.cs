using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIUserValidation.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    IdentificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreME", x => x.IdentificationId);
                });

            migrationBuilder.CreateTable(
                name: "IdClientME",
                schema: "UVA",
                columns: table => new
                {
                    IdentyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentiType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdClientME", x => x.IdentyId);
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
                    RelationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationShME", x => x.RelationId);
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
                    RolID = table.Column<int>(type: "int", nullable: true),
                    IdentificationId = table.Column<int>(type: "int", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenreIdIdentificationId = table.Column<int>(type: "int", nullable: true),
                    RelatIdRelationId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientME", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientME_GenreME_GenreIdIdentificationId",
                        column: x => x.GenreIdIdentificationId,
                        principalSchema: "UVA",
                        principalTable: "GenreME",
                        principalColumn: "IdentificationId");
                    table.ForeignKey(
                        name: "FK_ClientME_IdentificationME_IdentificationId",
                        column: x => x.IdentificationId,
                        principalSchema: "UVA",
                        principalTable: "IdentificationME",
                        principalColumn: "IdentificationId");
                    table.ForeignKey(
                        name: "FK_ClientME_RelationShME_RelatIdRelationId",
                        column: x => x.RelatIdRelationId,
                        principalSchema: "UVA",
                        principalTable: "RelationShME",
                        principalColumn: "RelationId");
                    table.ForeignKey(
                        name: "FK_ClientME_RoleME_RolID",
                        column: x => x.RolID,
                        principalSchema: "UVA",
                        principalTable: "RoleME",
                        principalColumn: "RolID");
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                schema: "UVA",
                columns: table => new
                {
                    UsuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuClientClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UsuId);
                    table.ForeignKey(
                        name: "FK_UserInfo_ClientME_UsuClientClientId",
                        column: x => x.UsuClientClientId,
                        principalSchema: "UVA",
                        principalTable: "ClientME",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_GenreIdIdentificationId",
                schema: "UVA",
                table: "ClientME",
                column: "GenreIdIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_IdentificationId",
                schema: "UVA",
                table: "ClientME",
                column: "IdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_RelatIdRelationId",
                schema: "UVA",
                table: "ClientME",
                column: "RelatIdRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientME_RolID",
                schema: "UVA",
                table: "ClientME",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UsuClientClientId",
                schema: "UVA",
                table: "UserInfo",
                column: "UsuClientClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdClientME",
                schema: "UVA");

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
