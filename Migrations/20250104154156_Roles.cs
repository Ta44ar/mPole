using Microsoft.EntityFrameworkCore.Migrations;

namespace mPole.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Role] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (NEWID(), 'User', 'USER', NEWID());
                INSERT INTO [dbo].[Role] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (NEWID(), 'Subscriber', 'SUBSCRIBER', NEWID());
                INSERT INTO [dbo].[Role] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (NEWID(), 'Instructor', 'INSTRUCTOR', NEWID());
                INSERT INTO [dbo].[Role] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (NEWID(), 'Admin', 'ADMIN', NEWID());
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [dbo].[Role] WHERE [Name] IN ('User', 'Subscriber', 'Instructor', 'Admin');
            ");
        }
    }
}