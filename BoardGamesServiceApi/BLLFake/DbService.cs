using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using BoardGamesServiceApi.DAL.EF.Context;
using BoardGamesServiceApi.DAL.Entities;

namespace BoardGamesServiceApi.BLL
{
    public class DbService : IDbService
    {
        private BoardGameContext _context;
        private IMemoryCache _cache;
        private MemoryCacheEntryOptions _cacheOptions;

        public DbService(BoardGameContext context, IMemoryCache cache, MemoryCacheEntryOptions cacheOptions)
        {
            _context = context ?? throw new ArgumentNullException($"The {nameof(context)} can not be nuul.");
            _cache = cache ?? throw new ArgumentNullException($"The {nameof(cache)} can not be nuul.");
            _cacheOptions = cacheOptions ?? throw new ArgumentNullException($"The {nameof(cacheOptions)} can not be nuul.");
        }

        public async Task<IEnumerable<BoardGame>> GetAllAsync()
        {
            return await _context.BoardGames.ToListAsync().ConfigureAwait(false);
        }

        public async Task<BoardGame> GetAsync(int id)
        {
            BoardGame game = null;

            if (!_cache.TryGetValue(id, out game))
            {
                game = await _context.BoardGames.FirstOrDefaultAsync(g => g.Id == id).ConfigureAwait(false);

                if (game != null)
                {
                    _cache.Set(id, game, _cacheOptions);
                }                
            }           

            return game;
        }

        public async Task<BoardGame> AddAsync(BoardGame game)
        {
            var addedGame = await _context.BoardGames.AddAsync(game).ConfigureAwait(false);

            int countChanges = await _context.SaveChangesAsync().ConfigureAwait(false);
            
            if (countChanges > 0)
            {
                _cache.Set(game.Id, addedGame.Entity, _cacheOptions);
            }

            return addedGame.Entity;
        }

        public async Task<BoardGame> DeleteAsync(int id)
        {
            var deletedGame = await _context.BoardGames.FirstOrDefaultAsync(g => g.Id == id).ConfigureAwait(false);

            if (deletedGame != null)
            {
                _context.BoardGames.Remove(deletedGame);

                int countChanges = await _context.SaveChangesAsync().ConfigureAwait(false);

                if (countChanges > 0)
                {
                    _cache.Remove(id);
                }
            }

            return deletedGame;
        }     
    }
}
