namespace MountainSocialNetwork.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixBugForUserProfilePictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfilePictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProfilePictures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
