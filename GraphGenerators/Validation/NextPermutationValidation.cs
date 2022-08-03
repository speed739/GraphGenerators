using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class NextPermutationValidation : AbstractValidator<GraphGeneratorModel>
    {
        public NextPermutationValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.PermutationsInArray)
            .Must(x => x.Count > 0);
            
        }

    }
}