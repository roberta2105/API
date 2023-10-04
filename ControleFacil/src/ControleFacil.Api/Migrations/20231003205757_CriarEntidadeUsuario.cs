using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "VARCHAR", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "naturezaDeLancamento",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    observacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_naturezaDeLancamento", x => x.id);
                    table.ForeignKey(
                        name: "FK_naturezaDeLancamento_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "apagar",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    valorPago = table.Column<double>(type: "double precision", nullable: true),
                    dataPagamento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idNaturezaDeLancamento = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    observacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    valorOriginal = table.Column<double>(type: "double precision", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    dataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "areceber",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    valorRecebido = table.Column<double>(type: "double precision", nullable: false),
                    dataRecebimento = table.Column<DateTime>(type: "timestamp", nullable: true),
                    idUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idNaturezaDeLancamento = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    observacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    valorOriginal = table.Column<double>(type: "double precision", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    dataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areceber", x => x.id);
                    table.ForeignKey(
                        name: "FK_areceber_naturezaDeLancamento_idNaturezaDeLancamento",
                        column: x => x.idNaturezaDeLancamento,
                        principalTable: "naturezaDeLancamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_areceber_usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apagar_idNaturezaDeLancamento",
                table: "apagar",
                column: "idNaturezaDeLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_apagar_idUsuario",
                table: "apagar",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_areceber_idNaturezaDeLancamento",
                table: "areceber",
                column: "idNaturezaDeLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_areceber_idUsuario",
                table: "areceber",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_naturezaDeLancamento_idUsuario",
                table: "naturezaDeLancamento",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apagar");

            migrationBuilder.DropTable(
                name: "areceber");

            migrationBuilder.DropTable(
                name: "naturezaDeLancamento");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
