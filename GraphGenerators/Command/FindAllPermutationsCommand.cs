using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraphGenerators
{
    public class FindAllPermutationsCommand : ICommand
    {
        private GraphValidation Validations;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var model = parameter as GraphGeneratorModel;
            GraphMethode methode = new GraphMethode();
            methode.GenerateMatrixPermutation(model);
            
            model.PermutationsCount = model.PermutationsInArray.Count;
            model.GraphSequence.Clear();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
