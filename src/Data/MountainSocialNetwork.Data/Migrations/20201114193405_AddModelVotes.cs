namespace MountainSocialNetwork.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddModelVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    NewsFeedId = table.Column<int>(nullable: false),
                    NewsFeedPostId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_NewsFeedPosts_NewsFeedPostId",
                        column: x => x.NewsFeedPostId,
                        principalTable: "NewsFeedPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_NewsFeedPostId",
                table: "Votes",
                column: "NewsFeedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
