using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMessenger.Infrastructure.EntityFramework.Migrations
{
    public partial class Alter_Workspace_Table_OwnerId_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Workspaces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Workspaces");
        }
    }
}
