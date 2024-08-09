using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMDbProject.Repositories;

    public class RankingsRepository : IRankingsRepository
    {
        private readonly ApplicationDbContext _context;

        public RankingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovieRankDTO>> GetTopRankedMoviesAsync()
        {
            var rankings = await _context.MovieRanks
                .Select(mr => new MovieRankDTO
                {
                    OMDBId = mr.OMDBId,
                    NumberofAdds = mr.NumberofAdds,
                    NumberOfRatings = mr.NumberOfRatings,
                    AverageRating = mr.AverageRating,
                    NumberOfLikes = mr.NumberOfLikes        
                })
                .ToListAsync();

            return rankings;
        }
    }

