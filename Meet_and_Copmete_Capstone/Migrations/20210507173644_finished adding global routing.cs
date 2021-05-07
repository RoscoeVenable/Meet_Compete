using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet_and_Copmete_Capstone.Migrations
{
    public partial class finishedaddingglobalrouting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90e22106-f064-4836-8b03-4b80877b08bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8e638d8-64d7-40e6-ac9d-f6f50d2141d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35a70546-13b4-42e5-999c-91f49a6dcaa6", "4bc6910f-012a-4093-975b-96e07062a9b7", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85437f3a-66c7-4604-b8de-eabf14ccbf19", "d303a048-7184-47ac-a48f-0f786e3d21bd", "EventPlaner", "EVENTPLANER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35a70546-13b4-42e5-999c-91f49a6dcaa6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85437f3a-66c7-4604-b8de-eabf14ccbf19");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90e22106-f064-4836-8b03-4b80877b08bd", "ca21a37c-f8e3-4bf2-8ab9-339322e552d0", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8e638d8-64d7-40e6-ac9d-f6f50d2141d8", "f74fb9d7-a8d8-4038-b786-9a29477fa3d8", "EventPlaner", "EVENTPLANER" });
        }
    }
}
