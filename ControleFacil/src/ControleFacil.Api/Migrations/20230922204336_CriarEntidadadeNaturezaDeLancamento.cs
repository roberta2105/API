using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadadeNaturezaDeLancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "naturezaDeLancamento",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    observacao = table.Column<string>(type: "VARCHAR", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_naturezaDeLancamento", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "naturezaDeLancamento");
        }
    }
}
