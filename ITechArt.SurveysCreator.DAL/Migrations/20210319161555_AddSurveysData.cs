using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.SurveysCreator.DAL.Migrations
{
    public partial class AddSurveysData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Options = table.Column<byte>(type: "tinyint", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2a220226-4849-4997-9432-67238d70514e",
                column: "ConcurrencyStamp",
                value: "92c17cc6-f0c2-4e58-90a6-d3c2bb42ffe1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a3a88ce7-9959-4a6d-85f6-a538e6f33171",
                column: "ConcurrencyStamp",
                value: "f07afbce-28dd-4c13-9aad-8d90e8e92e3f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "366238c0-7269-4345-a837-35c87843ec49", "AQAAAAEAACcQAAAAEJDbQpueoicdSqMCtgsTEToxU/fXaBJX5/4cdgGtftqiNGQFsrQZvq12QEmIbelBYw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SurveyId",
                table: "Pages",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PageId",
                table: "Questions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_AuthorId",
                table: "Surveys",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2a220226-4849-4997-9432-67238d70514e",
                column: "ConcurrencyStamp",
                value: "f8043a33-93ce-4a7c-b577-da5d33dea340");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a3a88ce7-9959-4a6d-85f6-a538e6f33171",
                column: "ConcurrencyStamp",
                value: "f76bb64b-eb1b-468d-98f3-bf6c6cabafa3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae5dac66-b6d6-4165-8ad9-9878df1cbfe8", "AQAAAAEAACcQAAAAEP3wTaU0pu7VPA/zCsGRcu5819mKxzodOZ1o37J5dEiPjsh7eBDAIp0vavsTPp9fPQ==" });
        }
    }
}
