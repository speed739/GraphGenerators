using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class DegreeValidation : AbstractValidator<GraphGeneratorModel>
    {
        public DegreeValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Degree)
            .NotEmpty()
            .GreaterThan(0);

        }
    }
}
