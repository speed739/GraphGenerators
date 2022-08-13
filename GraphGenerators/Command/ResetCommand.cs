using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GraphGenerators
{
    public class ResetCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            var model = parameter as GraphGeneratorModel;

            model.counter = 0;
            model.MatrixVisable = System.Windows.Visibility.Hidden;
            model.GraphSequencePreview = String.Empty;
            model.GraphSequence.Clear();
            model.GraphSequenceCopy.Clear();
            model.PermutationsCount = 0;
            model.PermutationsInArray.Clear();

            if (!(model.Permutations == null))
                model.Permutations.Clear();

            model.ImageInBytes = null;
            BitmapImage myBitmapImage = new BitmapImage();
            model.Image = myBitmapImage;
            model.GraphMatrix = null;

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
