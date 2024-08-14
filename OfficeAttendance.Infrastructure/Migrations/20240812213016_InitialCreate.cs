using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OfficeAttendance.Infrastructure.Migrations; 
/// <inheritdoc />
public partial class InitialCreate : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirstName = table.Column<string>(type: "text", nullable: false),
                LastName = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Employees", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Attendances",
            columns: table => new {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                EmployeeId = table.Column<int>(type: "integer", nullable: false),
                Date = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Attendances", x => x.Id);
                table.ForeignKey(
                    name: "FK_Attendances_Employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Attendances_EmployeeId",
            table: "Attendances",
            column: "EmployeeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Attendances");

        migrationBuilder.DropTable(
            name: "Employees");
    }
}
