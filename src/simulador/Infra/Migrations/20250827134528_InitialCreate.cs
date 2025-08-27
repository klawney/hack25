using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simulacoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdSimulacao = table.Column<long>(type: "INTEGER", nullable: false),
                    CodigoProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "TEXT", nullable: false),
                    TaxaJuros = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultadosSimulacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    SimulacaoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosSimulacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosSimulacao_Simulacoes_SimulacaoId",
                        column: x => x.SimulacaoId,
                        principalTable: "Simulacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorAmortizacao = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    ValorJuros = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    ValorPrestacao = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    ResultadoSimulacaoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcelas_ResultadosSimulacao_ResultadoSimulacaoId",
                        column: x => x.ResultadoSimulacaoId,
                        principalTable: "ResultadosSimulacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_ResultadoSimulacaoId",
                table: "Parcelas",
                column: "ResultadoSimulacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosSimulacao_SimulacaoId",
                table: "ResultadosSimulacao",
                column: "SimulacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "ResultadosSimulacao");

            migrationBuilder.DropTable(
                name: "Simulacoes");
        }
    }
}
