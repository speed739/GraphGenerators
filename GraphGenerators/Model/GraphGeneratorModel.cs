using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace GraphGenerators
{
    public class GraphGeneratorModel : INotifyPropertyChanged
    {

        private List<int> _graphSequence;
        private int _degree;
        private string _graphSequenceView;
        private ImageSource _image;
        private byte[] _imageInBytes;
        private DataView _matrixView;
        private int[,] _graphMatrix;
        private Visibility _matrixVisable;
        private List<int> _graphSequenceCopy;
        private List<List<string>> _permutations;
        private int _counter = 0;
        private int _permutationsCount;
        private List<int[,]> _permutationsInArray;

        public GraphGeneratorModel()
        {
            _graphSequence = new List<int>();
            _graphSequenceCopy = new List<int>();
            _permutationsInArray = new List<int[,]>();
            _matrixView = new DataView();
            _matrixVisable = Visibility.Hidden;

        }
        public int counter
        {
            get => _counter;
            set
            {
                if (value != _counter)
                {
                    _counter = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<int> GraphSequence
        {
            get => _graphSequence;
            set
            {
                if (value != _graphSequence)
                {
                    _graphSequence = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<int> GraphSequenceCopy
        {
            get => _graphSequenceCopy;
            set
            {
                if (value != _graphSequenceCopy)
                {
                    _graphSequenceCopy = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<int[,]> PermutationsInArray
        {
            get => _permutationsInArray;
            set
            {
                if (value != _permutationsInArray)
                {
                    _permutationsInArray = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<List<string>> Permutations
        {
            get => _permutations;
            set
            {
                if (value != _permutations)
                {
                    _permutations = value;
                    OnPropertyChanged();
                }
            }
        }
        public int PermutationsCount
        {
            get => _permutationsCount;
            set
            {
                if (value != _permutationsCount)
                {
                    _permutationsCount = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GraphSequencePreview
        {
            get => _graphSequenceView;
            set
            {
                if (value != _graphSequenceView)
                {
                    _graphSequenceView = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility MatrixVisable
        {
            get => _matrixVisable;
            set
            {
                if (value != _matrixVisable)
                {
                    _matrixVisable = value;
                    OnPropertyChanged();
                }
            }
        }
        public ImageSource Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }
        public DataView MatrixView
        {
            get => _matrixView;
            set
            {
                if (value != _matrixView)
                {
                    _matrixView = value;
                    OnPropertyChanged();
                }
            }
        }
        public int[,] GraphMatrix
        {
            get => _graphMatrix;
            set
            {
                if (value != _graphMatrix)
                {
                    _graphMatrix = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Degree
        {
            get => _degree;
            set
            {
                if (value != _degree)
                {
                    _degree = value;
                    OnPropertyChanged();
                }
            }
        }
        public byte[] ImageInBytes
        {
            get => _imageInBytes;
            set
            {
                if (value != _imageInBytes)
                {
                    _imageInBytes = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}


