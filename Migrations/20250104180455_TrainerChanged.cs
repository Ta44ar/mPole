using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class TrainerChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trainer",
                schema: "dbo",
                table: "Class");

            migrationBuilder.AddColumn<string>(
                name: "TrainerId",
                schema: "dbo",
                table: "Class",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Class_TrainerId",
                schema: "dbo",
                table: "Class",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_User_TrainerId",
                schema: "dbo",
                table: "Class",
                column: "TrainerId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_User_TrainerId",
                schema: "dbo",
                table: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Class_TrainerId",
                schema: "dbo",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                schema: "dbo",
                table: "Class");

            migrationBuilder.AddColumn<string>(
                name: "Trainer",
                schema: "dbo",
                table: "Class",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
