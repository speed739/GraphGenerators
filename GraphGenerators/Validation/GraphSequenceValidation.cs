using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class GraphSequenceValidation : AbstractValidator<GraphGeneratorModel>
    {

        public GraphSequenceValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.GraphSequence)
            .NotEmpty();
           
        }
    }
}
