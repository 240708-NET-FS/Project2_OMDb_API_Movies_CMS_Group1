using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovieRanksView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId_UserMovieId",
                table: "Likes",
                columns: new[] { "UserId", "UserMovieId" },
                unique: true);

            migrationBuilder.Sql(@"
            CREATE VIEW MovieRanks AS
            SELECT 
                UM.OMDBId,
                COUNT(*) AS NumberofAdds,
                COUNT(UM.UserRating) AS NumberOfRatings,
                AVG(UM.UserRating) AS AverageRating,
                COALESCE(COUNT(L.LikeId), 0) AS NumberOfLikes
            FROM 
                UserMovies UM
            LEFT JOIN 
                Likes L ON UM.UserMovieId = L.UserMovieId
            GROUP BY 
                UM.OMDBId;
        ");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId_UserMovieId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");


            migrationBuilder.Sql("DROP VIEW IF EXISTS MovieRanks;");

        }
    }
}
