using BoardGamesServiceApi.DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesServiceApi.DAL.Validators
{
    public class BoardGameValidator : AbstractValidator<BoardGame>
    {
        public BoardGameValidator()
        {
            RuleFor(g => g.Description).NotNull();
            RuleFor(g => g.Price).GreaterThan(0.0M);
        }
    }
}
