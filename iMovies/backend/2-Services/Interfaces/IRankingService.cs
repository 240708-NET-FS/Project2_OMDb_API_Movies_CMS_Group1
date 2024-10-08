using OMDbProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Services.Interfaces;

    public interface IRankingService
    {
        Task<List<MovieRankDTO>> GetTopRankedMoviesAsync();
    }

