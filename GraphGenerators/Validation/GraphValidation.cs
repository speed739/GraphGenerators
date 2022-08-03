using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class GraphValidation : AbstractValidator<GraphGeneratorModel>
    {

        public GraphValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.GraphSequence)
            .NotEmpty();
           
        }
    }
}
