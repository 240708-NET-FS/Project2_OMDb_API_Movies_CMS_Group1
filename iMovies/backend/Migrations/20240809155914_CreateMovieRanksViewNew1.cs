using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovieRanksViewNew1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        migrationBuilder.Sql("DROP VIEW IF EXISTS MovieRanks;");


        }
    }
}
