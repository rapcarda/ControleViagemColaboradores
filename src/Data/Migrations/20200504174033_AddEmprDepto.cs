using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddEmprDepto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Empresa_EmpresaId",
                table: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_EmpresaId",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Departamento");

            migrationBuilder.CreateTable(
                name: "EmprDept",
                columns: table => new
                {
                    EmpresaId = table.Column<Guid>(nullable: false),
                    DepartamentoId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprDept", x => new { x.EmpresaId, x.DepartamentoId });
                    table.ForeignKey(
                        name: "FK_EmprDept_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmprDept_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmprDept_DepartamentoId",
                table: "EmprDept",
                column: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmprDept");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Departamento",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_EmpresaId",
                table: "Departamento",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Empresa_EmpresaId",
                table: "Departamento",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
