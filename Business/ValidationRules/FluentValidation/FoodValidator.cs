using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class FoodValidator : AbstractValidator<Food>
    {
        public FoodValidator()
        {
            RuleFor(f => f.FoodName).NotEmpty();
            RuleFor(f => f.FoodName).MinimumLength(2).WithMessage("Yemek adı en az 2 karakter olmalı");
        }
    }
}
