using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeineErsteAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charaktere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Element = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Waffentyp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sterne = table.Column<int>(type: "int", nullable: false),
                    HabIch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charaktere", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charaktere");
        }
    }
}
