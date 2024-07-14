using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech.Challenge4.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tabela_Sala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Capacidade = table.Column<int>(type: "INT", nullable: false),
                    PrecoHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoworkingId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sala_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sala_CoworkingId",
                table: "Sala",
                column: "CoworkingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sala");
        }
    }
}
