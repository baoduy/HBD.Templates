using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TEMP.Infra.Migrations
{
    public partial class SetupDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pro");

            migrationBuilder.EnsureSchema(
                name: "seq");

            migrationBuilder.CreateSequence<int>(
                name: "Sequence_Membership",
                schema: "seq",
                maxValue: 99999L,
                cyclic: true);

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "pro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDay = table.Column<DateTime>(type: "Date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MembershipNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DataKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "pro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DataKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "pro",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "pro",
                table: "Profiles",
                columns: new[] { "Id", "Avatar", "BirthDay", "CreatedBy", "CreatedOn", "DataKey", "Email", "MembershipNo", "Name", "Phone", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("a6b50327-160e-423c-9c0b-c125588e6025"), null, null, "System", new DateTimeOffset(new DateTime(2022, 6, 26, 20, 35, 4, 237, DateTimeKind.Unspecified).AddTicks(45), new TimeSpan(0, 8, 0, 0, 0)), null, "abc@gmail.com", "MS12345", "Steven Hoang", "123456789", "System", new DateTimeOffset(new DateTime(2022, 6, 26, 20, 35, 4, 237, DateTimeKind.Unspecified).AddTicks(75), new TimeSpan(0, 8, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedBy",
                schema: "pro",
                table: "Employees",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedOn",
                schema: "pro",
                table: "Employees",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProfileId",
                schema: "pro",
                table: "Employees",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedBy",
                schema: "pro",
                table: "Employees",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedOn",
                schema: "pro",
                table: "Employees",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CreatedBy",
                schema: "pro",
                table: "Profiles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CreatedOn",
                schema: "pro",
                table: "Profiles",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_Email",
                schema: "pro",
                table: "Profiles",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_MembershipNo",
                schema: "pro",
                table: "Profiles",
                column: "MembershipNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UpdatedBy",
                schema: "pro",
                table: "Profiles",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UpdatedOn",
                schema: "pro",
                table: "Profiles",
                column: "UpdatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "pro");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "pro");

            migrationBuilder.DropSequence(
                name: "Sequence_Membership",
                schema: "seq");
        }
    }
}
