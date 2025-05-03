using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviçohospital.Migrations
{
    /// <inheritdoc />
    public partial class migração10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "administracaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cargo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administracaos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRM = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Especialidade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ProfissionalSaudeId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoEvento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacoes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historicos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historicos_Profissionais_ProfissionalSaudeId",
                        column: x => x.ProfissionalSaudeId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    ProfissionalSaudeId = table.Column<int>(type: "int", nullable: true),
                    AdministracaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuarios_Profissionais_ProfissionalSaudeId",
                        column: x => x.ProfissionalSaudeId,
                        principalTable: "Profissionais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuarios_administracaos_AdministracaoId",
                        column: x => x.AdministracaoId,
                        principalTable: "administracaos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ProfissionalSaudeId = table.Column<int>(type: "int", nullable: false),
                    HistoricoId = table.Column<int>(type: "int", nullable: true),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Prontuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Historicos_HistoricoId",
                        column: x => x.HistoricoId,
                        principalTable: "Historicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Profissionais_ProfissionalSaudeId",
                        column: x => x.ProfissionalSaudeId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "seguranca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Acao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seguranca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_seguranca_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prescricoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsultaID = table.Column<int>(type: "int", nullable: false),
                    Medicamento = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dosagem = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataPrescricao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HistoricoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescricoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Consultas_ConsultaID",
                        column: x => x.ConsultaID,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Historicos_HistoricoId",
                        column: x => x.HistoricoId,
                        principalTable: "Historicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_HistoricoId",
                table: "Consultas",
                column: "HistoricoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ProfissionalSaudeId",
                table: "Consultas",
                column: "ProfissionalSaudeId");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_PacienteId",
                table: "Historicos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_ProfissionalSaudeId",
                table: "Historicos",
                column: "ProfissionalSaudeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_ConsultaID",
                table: "Prescricoes",
                column: "ConsultaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_HistoricoId",
                table: "Prescricoes",
                column: "HistoricoId");

            migrationBuilder.CreateIndex(
                name: "IX_seguranca_UsuarioId",
                table: "seguranca",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_AdministracaoId",
                table: "usuarios",
                column: "AdministracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_PacienteId",
                table: "usuarios",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_ProfissionalSaudeId",
                table: "usuarios",
                column: "ProfissionalSaudeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescricoes");

            migrationBuilder.DropTable(
                name: "seguranca");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "administracaos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Profissionais");
        }
    }
}
