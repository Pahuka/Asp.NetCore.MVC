using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asp.NetCore.MVC.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbTableIncidentFroms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableIncidentFroms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbTableIncidentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncidentNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableIncidentHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbTableIncidents",
                columns: table => new
                {
                    IncidentNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonTitleId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentFromId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableIncidents", x => x.IncidentNumber);
                });

            migrationBuilder.CreateTable(
                name: "DbTableReasonTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableReasonTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbTableUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DbTableIncidentFroms",
                columns: new[] { "Id", "EditingDate", "From" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 6, 21, 38, 19, 341, DateTimeKind.Local).AddTicks(8982), "Колл-центр" },
                    { 2, new DateTime(2023, 6, 6, 21, 38, 19, 341, DateTimeKind.Local).AddTicks(8984), "Сервисный центр" }
                });

            migrationBuilder.InsertData(
                table: "DbTableReasonTitles",
                columns: new[] { "Id", "EditingDate", "Reason" },
                values: new object[] { 1, new DateTime(2023, 6, 6, 21, 38, 19, 341, DateTimeKind.Local).AddTicks(8954), "Тестовая причина" });

            migrationBuilder.InsertData(
                table: "DbTableUsers",
                columns: new[] { "Id", "EditingDate", "FirstName", "IsAdministrator", "LastName", "Login", "Password" },
                values: new object[] { new Guid("1f8c887f-fcaf-419d-aaa6-9aa2f8b764f7"), new DateTime(2023, 6, 6, 21, 38, 19, 341, DateTimeKind.Local).AddTicks(8805), "Admin", true, "Admin", "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTableIncidentFroms");

            migrationBuilder.DropTable(
                name: "DbTableIncidentHistories");

            migrationBuilder.DropTable(
                name: "DbTableIncidents");

            migrationBuilder.DropTable(
                name: "DbTableReasonTitles");

            migrationBuilder.DropTable(
                name: "DbTableUsers");
        }
    }
}
