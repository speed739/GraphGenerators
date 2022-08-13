using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class GraphMatrixValidation : AbstractValidator<GraphGeneratorModel>
    {
        public GraphMatrixValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.GraphMatrix)
            .NotEmpty()
            .NotNull();               

        }
    }
}
