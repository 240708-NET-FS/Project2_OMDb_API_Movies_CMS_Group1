using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Services;

    public class RankingService : IRankingService
    {
        private readonly IRankingsRepository _rankingsRepository;

        public RankingService(IRankingsRepository rankingsRepository)
        {
            _rankingsRepository = rankingsRepository;
        }

        public async Task<List<MovieRankDTO>> GetTopRankedMoviesAsync()
        {
            return await _rankingsRepository.GetTopRankedMoviesAsync();
        }
    }

