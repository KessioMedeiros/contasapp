using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TIPO = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "USUARIO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CONTA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DATA = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OBSERVACOES = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CATEGORIA_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USUARIO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CONTA_CATEGORIA_CATEGORIA_ID",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CONTA_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_UsuarioId",
                table: "CATEGORIA",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_CATEGORIA_ID",
                table: "CONTA",
                column: "CATEGORIA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_USUARIO_ID",
                table: "CONTA",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTA");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
