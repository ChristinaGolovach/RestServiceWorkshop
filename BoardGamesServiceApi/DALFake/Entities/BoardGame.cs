using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesServiceApi.DAL.Entities
{
    public class BoardGame
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
