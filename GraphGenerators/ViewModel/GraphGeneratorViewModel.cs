using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerators
{
    public class GraphGeneratorViewModel
    {
        public GraphGeneratorModel Model { get; set; }
        public IsGraphicCommand IsGraphicCommand { get; set; }
        public AddToSequenceCommand AddToSequenceCommand { get; set; }
        public FindAllPermutationsCommand FindAllPermutationsCommand { get; set; }
        public NextPermutationCommand NextPermutationCommand { get; set; }
        public ResetCommand ResetCommand { get; set; }

        public GraphGeneratorViewModel()
        {
            Model = new GraphGeneratorModel();
            IsGraphicCommand = new IsGraphicCommand();
            AddToSequenceCommand = new AddToSequenceCommand();
            FindAllPermutationsCommand = new FindAllPermutationsCommand();
            NextPermutationCommand = new NextPermutationCommand();
            ResetCommand = new ResetCommand(); 
        }

    }
}
