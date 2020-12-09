namespace MountainSocialNetwork.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeNameFromTimeLineToNewsFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeLinePosts");

            migrationBuilder.CreateTable(
                name: "NewsFeedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsFeedPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedPosts_IsDeleted",
                table: "NewsFeedPosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedPosts_UserId",
                table: "NewsFeedPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsFeedPosts");

            migrationBuilder.CreateTable(
                name: "TimeLinePosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLinePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLinePosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeLinePosts_IsDeleted",
                table: "TimeLinePosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLinePosts_UserId",
                table: "TimeLinePosts",
                column: "UserId");
        }
    }
}
