using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class trainingLevelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                schema: "dbo",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                schema: "dbo",
                table: "Training");
        }
    }
}
