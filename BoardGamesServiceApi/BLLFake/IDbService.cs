using BoardGamesServiceApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesServiceApi.BLL
{
    public interface IDbService
    {
        Task<IEnumerable<BoardGame>> GetAllAsync();
        Task<BoardGame> GetAsync(int id);        
        Task<BoardGame> AddAsync(BoardGame game);
        //Task<BoardGame> UpdateAsync(BoardGame game);
        Task<BoardGame> DeleteAsync(int id);
    }
}
