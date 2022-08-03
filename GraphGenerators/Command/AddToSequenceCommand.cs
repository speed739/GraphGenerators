using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphGenerators
{
    public class AddToSequenceCommand : ICommand
    {
        private DegreeValidation Validation = new DegreeValidation();
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as GraphGeneratorModel;
            model.GraphSequence.Add(model.Degree);
            model.GraphSequence.Sort();
            model.GraphSequence.Reverse();
            model.GraphSequencePreview = "[ "+string.Join(",",model.GraphSequence) +" ]";
            model.Degree = 0;

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
