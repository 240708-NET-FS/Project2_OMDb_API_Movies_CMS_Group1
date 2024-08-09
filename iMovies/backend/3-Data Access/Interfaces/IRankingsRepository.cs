using OMDbProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Repositories.Interfaces
{
    public interface IRankingsRepository
    {
        Task<List<MovieRankDTO>> GetTopRankedMoviesAsync();
    }
}
