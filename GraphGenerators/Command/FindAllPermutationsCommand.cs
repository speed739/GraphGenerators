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
        private GraphMatrixValidation Validations;
        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                var result = parameter as GraphGeneratorModel;
                Validations = new GraphMatrixValidation();
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
            GraphMethode methode = new GraphMethode();
            methode.GenerateMatrixPermutation(model);
            
            model.PermutationsCount = model.PermutationsInArray.Count;
            MessageBox.Show("Program znalazł [" + model.PermutationsCount + "] wariacji podanego ciągu","Information",
                            MessageBoxButton.OK,MessageBoxImage.Information);
            model.GraphSequence.Clear();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
