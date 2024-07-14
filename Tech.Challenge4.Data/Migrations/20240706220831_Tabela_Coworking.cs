using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech.Challenge4.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tabela_Coworking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coworking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    HoraAbertura = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    HoraFechamento = table.Column<TimeSpan>(type: "TIME", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coworking", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coworking");
        }
    }
}
