using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MountainSocialNetwork.Data.Migrations
{
    public partial class AddModelNewsFeedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsFeedComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    NewsFeedPostId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsFeedComments_NewsFeedPosts_NewsFeedPostId",
                        column: x => x.NewsFeedPostId,
                        principalTable: "NewsFeedPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeedComments_NewsFeedComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "NewsFeedComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeedComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedComments_IsDeleted",
                table: "NewsFeedComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedComments_NewsFeedPostId",
                table: "NewsFeedComments",
                column: "NewsFeedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedComments_ParentId",
                table: "NewsFeedComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedComments_UserId",
                table: "NewsFeedComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsFeedComments");
        }
    }
}
