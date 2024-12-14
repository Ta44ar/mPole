using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class entityTypeConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Move_Training_TrainingId",
                schema: "dbo",
                table: "Move");

            migrationBuilder.DropIndex(
                name: "IX_Move_TrainingId",
                schema: "dbo",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "dbo",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "dbo",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "RegisteredUsers",
                schema: "dbo",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Trainer",
                schema: "dbo",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                schema: "dbo",
                table: "Move");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Move",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Move",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                schema: "dbo",
                table: "Image",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Class",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Trainer = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    RegisteredUsers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalSchema: "dbo",
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveTraining",
                schema: "dbo",
                columns: table => new
                {
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveTraining", x => new { x.MoveId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_MoveTraining_Move_MoveId",
                        column: x => x.MoveId,
                        principalSchema: "dbo",
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveTraining_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalSchema: "dbo",
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_TrainingId",
                schema: "dbo",
                table: "Class",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTraining_TrainingId",
                schema: "dbo",
                table: "MoveTraining",
                column: "TrainingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Class",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MoveTraining",
                schema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "Training",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredUsers",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Trainer",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Move",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Move",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.AddColumn<int>(
                name: "TrainingId",
                schema: "dbo",
                table: "Move",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                schema: "dbo",
                table: "Image",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

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
    }
}
