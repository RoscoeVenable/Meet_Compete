using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet_and_Copmete_Capstone.Migrations
{
    public partial class Changedtimefromstringtoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18bbe7d0-3158-4393-9589-a090b2607edd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76907f89-d36f-4a66-b7ae-30c09f8eef9f");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c663873e-fa7c-4396-9f60-cf079ed53e67", "f5233196-5865-4188-8ad7-a4a7ae9505e7", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebb481b0-5b84-4be8-8267-d8ef6d8121a7", "7d859c5d-2208-459e-a04e-f50c79aa5a90", "EventPlaner", "EVENTPLANER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c663873e-fa7c-4396-9f60-cf079ed53e67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebb481b0-5b84-4be8-8267-d8ef6d8121a7");

            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18bbe7d0-3158-4393-9589-a090b2607edd", "26ca2864-52f6-4e97-910f-f1227a96a626", "Eventee", "EVENTEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76907f89-d36f-4a66-b7ae-30c09f8eef9f", "1ba2ff35-c810-45b0-b50f-baf85bef36c0", "EventPlaner", "EVENTPLANER" });
        }
    }
}
