namespace MountainSocialNetwork.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeVoteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Votes");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpVote",
                table: "Votes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpVote",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
