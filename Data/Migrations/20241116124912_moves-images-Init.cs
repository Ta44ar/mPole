using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mPole.Migrations
{
    /// <inheritdoc />
    public partial class movesimagesInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoleDanceMove",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoleDanceMove", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoleDanceMoveImage",
                schema: "dbo",
                columns: table => new
                {
                    PoleDanceMoveId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoleDanceMoveImage", x => new { x.PoleDanceMoveId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_PoleDanceMoveImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "dbo",
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoleDanceMoveImage_PoleDanceMove_PoleDanceMoveId",
                        column: x => x.PoleDanceMoveId,
                        principalSchema: "dbo",
                        principalTable: "PoleDanceMove",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoleDanceMoveImage_ImageId",
                schema: "dbo",
                table: "PoleDanceMoveImage",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoleDanceMoveImage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PoleDanceMove",
                schema: "dbo");
        }
    }
}
