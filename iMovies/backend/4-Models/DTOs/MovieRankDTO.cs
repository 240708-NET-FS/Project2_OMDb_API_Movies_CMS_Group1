namespace OMDbProject.Models.DTOs;

    public class MovieRankDTO
    {
        public string? OMDBId { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberofAdds { get; set; }
        public int NumberOfRatings { get; set; }
        public decimal AverageRating { get; set; }
         
    }

