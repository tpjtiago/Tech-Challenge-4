using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech.Challenge4.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tabela_Reserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataReserva = table.Column<DateOnly>(type: "DATE", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    HoraFinal = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    StatusReserva = table.Column<int>(type: "INT", nullable: false),
                    Comparecimento = table.Column<bool>(type: "BIT", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    StatusPagamento = table.Column<int>(type: "INT", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_CustomerID",
                table: "Reserva",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_SalaID",
                table: "Reserva",
                column: "SalaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");
        }
    }
}
