using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class classesconfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredUsers",
                schema: "dbo",
                table: "Class");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClass",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredUsers",
                schema: "dbo",
                table: "Class",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
