using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMessenger.Infrastructure.EntityFramework.Migrations
{
    public partial class dataInitializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkspaceRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Owner" });

            migrationBuilder.InsertData(
                table: "WorkspaceRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.InsertData(
                table: "WorkspaceRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkspaceRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkspaceRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkspaceRoles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
