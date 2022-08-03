using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphGenerators
{
    public class NextPermutationCommand : ICommand
    {
        private NextPermutationValidation Validations;
        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                var result = parameter as GraphGeneratorModel;
                Validations = new NextPermutationValidation();
                if (Validations.Validate(result).IsValid)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public void Execute(object parameter)
        {
            var model = parameter as GraphGeneratorModel;
            var methode = new GraphMethode();

            if (!(model.PermutationsInArray.Count - 1 > model.counter))
                model.counter = 0;
            else
                model.counter++;

            methode.NextPermutation(model);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
