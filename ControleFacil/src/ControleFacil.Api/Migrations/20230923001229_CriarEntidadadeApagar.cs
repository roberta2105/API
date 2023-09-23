using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadadeApagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "idUsuario",
                table: "naturezaDeLancamento",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "apagar",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idNaturezaDeLancamento = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    valorOriginal = table.Column<double>(type: "double precision", nullable: false),
                    valorPago = table.Column<double>(type: "double precision", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataReferencia = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataPagamento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataInativacao = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apagar", x => x.id);
                    table.ForeignKey(
                        name: "FK_apagar_naturezaDeLancamento_idNaturezaDeLancamento",
                        column: x => x.idNaturezaDeLancamento,
                        principalTable: "naturezaDeLancamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_apagar_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_naturezaDeLancamento_idUsuario",
                table: "naturezaDeLancamento",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_apagar_idNaturezaDeLancamento",
                table: "apagar",
                column: "idNaturezaDeLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_apagar_idUsuario",
                table: "apagar",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_naturezaDeLancamento_usuario_idUsuario",
                table: "naturezaDeLancamento",
                column: "idUsuario",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_naturezaDeLancamento_usuario_idUsuario",
                table: "naturezaDeLancamento");

            migrationBuilder.DropTable(
                name: "apagar");

            migrationBuilder.DropIndex(
                name: "IX_naturezaDeLancamento_idUsuario",
                table: "naturezaDeLancamento");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "naturezaDeLancamento");
        }
    }
}
