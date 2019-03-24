using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesServiceApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardGamesServiceApi.DAL.Entities;

namespace BoardGamesServiceApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private IDbService _dbService;

        public BoardGamesController(IDbService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException($"The {nameof(dbService)} can not be null.");
        }

        /// <summary>
        /// Returns a list of board games.
        /// </summary>
        /// <returns>All board games.</returns>
        /// <response code="200">A list of board games.</response>
        /// <response code="500">If Internal Server Error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllBoardGames()
        {
            var games =  await _dbService.GetAllAsync();

            return Ok(games);
        }

        /// <summary>
        /// Returns a specific board game.
        /// </summary>
        /// <param name="id">The id of board game.</param>
        /// <returns>A specific board game.</returns>
        /// <response code="200">A specific board game.</response>
        /// <response code="404">If a game is not found.</response>
        /// <response code="500">If Internal Server Error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBoardGameById(int id)
        {
            var game = await _dbService.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        /// <summary>
        /// Creates a board game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Board game
        ///     {              
        ///        "description": "game description",
        ///        "price": 15.00
        ///     }
        ///
        /// </remarks>
        /// <param name="boardGame">A new board game.</param>
        /// <returns>A newly created board game.</returns>
        /// <response code="200">A newly created board game.</response>
        /// <response code="400">If a board game is null.</response>
        /// <response code="500">If Internal Server Error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddBoardGame([FromBody] BoardGame boardGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var game = await _dbService.AddAsync(boardGame);

            return Ok(game);
        }

        /// <summary>
        /// Deletes a specific board game.
        /// </summary>
        /// <param name="id">Board game id.</param>
        /// <returns>No content.</returns>
        /// <response code="204">No content.</response>
        /// <response code="404">If a game is not found.</response>
        /// <response code="500">If Internal Server Error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBoardGame(int id)
        {
            var deletedGame = await _dbService.DeleteAsync(id);

            if (deletedGame == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}