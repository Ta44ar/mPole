using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsRegistrationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClass",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Registration",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Class_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_ClassId",
                schema: "dbo",
                table: "Registration",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_UserId",
                schema: "dbo",
                table: "Registration",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "UserClass",
                schema: "dbo",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClass", x => new { x.ClassId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserClass_Class_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClass_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClass_UserId",
                schema: "dbo",
                table: "UserClass",
                column: "UserId");
        }
    }
}