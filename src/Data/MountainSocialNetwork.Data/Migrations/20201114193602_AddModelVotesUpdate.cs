namespace MountainSocialNetwork.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddModelVotesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                table: "Votes");

            migrationBuilder.AlterColumn<int>(
                name: "NewsFeedPostId",
                table: "Votes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewsFeedPostId",
                table: "Votes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "NewsFeedId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
