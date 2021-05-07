using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet_and_Copmete_Capstone.Migrations
{
    public partial class globalrouting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98f59591-1229-491d-beaa-05fa39c0f047");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b28bb38-6d91-4825-946b-c1c69031d2d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90e22106-f064-4836-8b03-4b80877b08bd", "ca21a37c-f8e3-4bf2-8ab9-339322e552d0", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8e638d8-64d7-40e6-ac9d-f6f50d2141d8", "f74fb9d7-a8d8-4038-b786-9a29477fa3d8", "EventPlaner", "EVENTPLANER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "98f59591-1229-491d-beaa-05fa39c0f047", "caf2cdf3-4031-44c1-bb0f-d5a034b8b436", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b28bb38-6d91-4825-946b-c1c69031d2d8", "e49e3419-6cb3-4bf7-a1a9-15005e6ae479", "EventPlaner", "EVENTPLANER" });
        }
    }
}
