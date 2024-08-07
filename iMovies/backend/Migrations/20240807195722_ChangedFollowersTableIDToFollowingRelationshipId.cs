using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFollowersTableIDToFollowingRelationshipId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Followers_UserId",
                table: "Followers");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "Followers",
                newName: "FollowingRelationshipId");

            migrationBuilder.AlterColumn<string>(
                name: "UserReview",
                table: "UserMovies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UserId_FollowerUserId",
                table: "Followers",
                columns: new[] { "UserId", "FollowerUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Followers_UserId_FollowerUserId",
                table: "Followers");

            migrationBuilder.RenameColumn(
                name: "FollowingRelationshipId",
                table: "Followers",
                newName: "FollowerId");

            migrationBuilder.AlterColumn<string>(
                name: "UserReview",
                table: "UserMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Followers_UserId",
                table: "Followers",
                column: "UserId");
        }
    }
}
