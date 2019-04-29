using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcBandas.Migrations
{
    public partial class modelo_concierto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Banda",
                table: "Banda");

            migrationBuilder.RenameTable(
                name: "Banda",
                newName: "Bandas");

            migrationBuilder.AlterColumn<string>(
                name: "Vocalista",
                table: "Bandas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Bandas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bandas",
                table: "Bandas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Conciertos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lugar = table.Column<string>(nullable: false),
                    BandaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conciertos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conciertos_Bandas_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conciertos_BandaId",
                table: "Conciertos",
                column: "BandaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conciertos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bandas",
                table: "Bandas");

            migrationBuilder.RenameTable(
                name: "Bandas",
                newName: "Banda");

            migrationBuilder.AlterColumn<string>(
                name: "Vocalista",
                table: "Banda",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Banda",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banda",
                table: "Banda",
                column: "Id");
        }
    }
}
