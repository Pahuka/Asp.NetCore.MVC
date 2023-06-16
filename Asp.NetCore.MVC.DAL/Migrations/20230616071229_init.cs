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

            migrationBuilder.CreateTable(
                name: "DbTableIncidents",
                columns: table => new
                {
                    IncidentNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbTableIncidentFromId = table.Column<int>(type: "int", nullable: false),
                    DbTableReasonTitleId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableIncidents", x => x.IncidentNumber);
                    table.ForeignKey(
                        name: "FK_DbTableIncidents_DbTableIncidentFroms_DbTableIncidentFromId",
                        column: x => x.DbTableIncidentFromId,
                        principalTable: "DbTableIncidentFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbTableIncidents_DbTableReasonTitles_DbTableReasonTitleId",
                        column: x => x.DbTableReasonTitleId,
                        principalTable: "DbTableReasonTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DbTableIncidentFroms",
                columns: new[] { "Id", "EditingDate", "From" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3289), "Все источники" },
                    { 2, new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3268), "Сервисный центр" },
                    { 3, new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3265), "Колл-центр" }
                });

            migrationBuilder.InsertData(
                table: "DbTableReasonTitles",
                columns: new[] { "Id", "EditingDate", "Reason" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3230), "Все причины" },
                    { 2, new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3232), "Тестовая причина" }
                });

            migrationBuilder.InsertData(
                table: "DbTableUsers",
                columns: new[] { "Id", "EditingDate", "FirstName", "IsAdministrator", "LastName", "Login", "Password" },
                values: new object[] { new Guid("65c9c733-2d13-4f40-8ed4-05a148087036"), new DateTime(2023, 6, 16, 12, 12, 29, 181, DateTimeKind.Local).AddTicks(3071), "Admin", true, "Admin", "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DbTableIncidents_DbTableIncidentFromId",
                table: "DbTableIncidents",
                column: "DbTableIncidentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_DbTableIncidents_DbTableReasonTitleId",
                table: "DbTableIncidents",
                column: "DbTableReasonTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTableIncidentHistories");

            migrationBuilder.DropTable(
                name: "DbTableIncidents");

            migrationBuilder.DropTable(
                name: "DbTableUsers");

            migrationBuilder.DropTable(
                name: "DbTableIncidentFroms");

            migrationBuilder.DropTable(
                name: "DbTableReasonTitles");
        }
    }
}
