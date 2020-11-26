using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMessenger.Infrastructure.EntityFramework.Migrations
{
    public partial class defaultDataChannelType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ChannelType_ChannelTypeId",
                table: "Channels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelType",
                table: "ChannelType");

            migrationBuilder.DropColumn(
                name: "ChannelTypeId",
                table: "ChannelType");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChannelType",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelType",
                table: "ChannelType",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ChannelType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Public" });

            migrationBuilder.InsertData(
                table: "ChannelType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Private" });

            migrationBuilder.InsertData(
                table: "ChannelType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Conversation" });

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_ChannelType_ChannelTypeId",
                table: "Channels",
                column: "ChannelTypeId",
                principalTable: "ChannelType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ChannelType_ChannelTypeId",
                table: "Channels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelType",
                table: "ChannelType");

            migrationBuilder.DeleteData(
                table: "ChannelType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChannelType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChannelType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChannelType");

            migrationBuilder.AddColumn<int>(
                name: "ChannelTypeId",
                table: "ChannelType",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelType",
                table: "ChannelType",
                column: "ChannelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_ChannelType_ChannelTypeId",
                table: "Channels",
                column: "ChannelTypeId",
                principalTable: "ChannelType",
                principalColumn: "ChannelTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
