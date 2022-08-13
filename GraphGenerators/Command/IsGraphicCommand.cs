using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphGenerators
{
    public class IsGraphicCommand : ICommand
    {
        private GraphSequenceValidation Validations;

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                var result = parameter as GraphGeneratorModel;
                Validations = new GraphSequenceValidation();
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
            model.counter = 0;
            GraphMethode methode = new GraphMethode();
            List<int> graphSequenceList = model.GraphSequence.ToList();
            model.GraphSequenceCopy = model.GraphSequence.ToList();

            if (methode.IsGraphic(graphSequenceList))
            {
                MessageBox.Show("Podany ciąg spełnia wymogi graficzności", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                model.PermutationsInArray.Clear();
                methode.CreateMatrix(model);
                methode.ConvertGraphToImage();
                methode.CreateMatrixView(model, model.GraphSequence);
                model.MatrixVisable = Visibility.Visible;
                methode.LoadImageToView(model);

                model.GraphSequence.Clear();
            }
            else
            {
                MessageBox.Show("Podany ciąg nie jest graficzny", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                model.GraphSequencePreview = string.Empty;
                model.GraphSequence.Clear();
                model.GraphSequenceCopy.Clear();
            }
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
