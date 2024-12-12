using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class trainingInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                schema: "dbo",
                table: "Move",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Training",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trainer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredUsers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Move_TrainingId",
                schema: "dbo",
                table: "Move",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Move_Training_TrainingId",
                schema: "dbo",
                table: "Move",
                column: "TrainingId",
                principalSchema: "dbo",
                principalTable: "Training",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Move_Training_TrainingId",
                schema: "dbo",
                table: "Move");

            migrationBuilder.DropTable(
                name: "Training",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Move_TrainingId",
                schema: "dbo",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                schema: "dbo",
                table: "Move");
        }
    }
}
