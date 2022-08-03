using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphGenerators
{
    public class GraphMethode
    {
        public bool IsGraphic(List<int> graphSequence)
        {
            Dictionary<int, List<int>> Edges = new Dictionary<int, List<int>>();

            bool result = false;
            graphSequence.Sort();
            graphSequence.Reverse();

            if (graphSequence.Any(x => x >= graphSequence.Count))
                return false;

            while (graphSequence.Any(x => x > 1))
            {
                var FirstVerti = graphSequence[0]; //przypisanie pierwszego wierzcholka do zmiennej

                graphSequence.Remove(graphSequence[0]); //usunieciee wierzcholka z listy

                for (int i = 0; i < FirstVerti; i++)
                {
                    graphSequence[i]--;      //Odjecie kolejno po jednym stopniu z pozostalych wierzcholkow w kolekcji
                }
                graphSequence.Sort();        //posortowanie kolekcji
                graphSequence.Reverse();

            }
            if (graphSequence.Sum() % 2 == 0)
            {
                result = true;
            }
            return result;
        }
        public void NextPermutation(GraphGeneratorModel model)
        {
            CreateGraph(model.PermutationsInArray[model.counter], AssignDegreesToNodes(model.GraphSequenceCopy));
            ConvertGraphToImage();
            CreateMatrixView(model, model.GraphSequenceCopy);
            model.MatrixVisable = Visibility.Visible;
            LoadImageToView(model);
        }
        public void LoadImageToView(GraphGeneratorModel model)
        {
            Image img = Image.FromFile(@"C:\Users\eci\Desktop\graph.png");
            var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);

            model.ImageInBytes = ms.ToArray();
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.StreamSource = new MemoryStream(model.ImageInBytes);
            myBitmapImage.EndInit();
            model.Image = myBitmapImage;
            img.Dispose();
        }
        public void ConvertGraphToImage()
        {
            var command = "/C dot -Tpng C:\\Users\\eci\\Desktop\\graph.dot -o C:\\Users\\eci\\Desktop\\graph.png";
            Process ps = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Verb = "runas";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = command;

            try
            {
                ps.StartInfo = startInfo;
                ps.Start();
                ps.WaitForExit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void CreateMatrix(GraphGeneratorModel model)
        {
            model.GraphMatrix = new int[model.GraphSequence.Count, model.GraphSequence.Count];
            //stworzenie macierzy 0,1 

            for (int i = 0; i < model.GraphSequence.Count; i++)
            {
                for (int j = 0; j < model.GraphSequence.Count; j++)
                {
                    var a = model.GraphSequence[i];
                    var b = model.GraphSequence[j];

                    if (model.GraphSequence[i] > 0 && model.GraphSequence[j] > 0 && i != j)
                    {
                        model.GraphMatrix[i, j] = 1;
                        model.GraphMatrix[j, i] = 1;
                        model.GraphSequence[i]--;
                        model.GraphSequence[j]--;
                    }
                }
            }
            //Sprawadzenie utworzonej macierzy pod katem zgodnosci z podana sekwencja
            if (MatrixValidation(model))
                CreateGraph(model.GraphMatrix, AssignDegreesToNodes(model.GraphSequence));
        }
        private bool MatrixValidation(GraphGeneratorModel model)
        {
            if (model.GraphSequence.Exists(x => x > 0))
            {
                var value = model.GraphSequence.First(x => x > 0);
                var index = model.GraphSequence.IndexOf(value);
                model.GraphSequence[index]--;
                model.GraphMatrix[index - 1, index] = 1;
                model.GraphMatrix[index, index - 1] = 1;
                //Sprawdzamy czy zgadza się ciag stopnia, jesli nie to go korygujemy

                for (int i = 0; i < model.GraphMatrix.GetLength(0); i++)
                {
                    var sum = 0;
                    for (int j = 0; j < model.GraphMatrix.GetLength(1); j++)
                    {
                        if (model.GraphMatrix[i, j] > 0)
                            sum++;
                    }

                    //case suma > odejmujemy, suma < dodajemy,suma = nic
                    if (sum != model.GraphSequenceCopy[i])
                    {
                        //Poprawić!!!
                        //MatrixCorrection(sum, model, index, i);
                    }
                }
                MatrixValidation(model);
            }
            //Czy to jest potrzebne ??

            //GenerateMatrixPermutation(model);
            return true;
        }
        public void GenerateMatrixPermutation(GraphGeneratorModel model)
        {
            model.Permutations = new List<List<string>>();
            string answer = string.Empty;

            for (int i = 0; i < model.GraphMatrix.GetLength(0); i++)
            {
                string s = string.Empty;
                for (int j = 0; j < model.GraphMatrix.GetLength(1); j++)
                {
                    if (i != j)
                        s += model.GraphMatrix[i, j];
                }
                List<string> list = new List<string>();
                model.Permutations.Add(list);

                Permute(s, answer, model, i);
            }
        }
        public void Permute(string s, string answer, GraphGeneratorModel model, int index)
        {
            if (s.Length == 0)
            {
                if (!model.Permutations[index].Contains(answer) && IsAnswerCorrect(model, answer, index))
                    model.Permutations[index].Add(answer);
                return;
            }

            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                String left_substr = s.Substring(0, i);
                String right_substr = s.Substring(i + 1);
                String rest = left_substr + right_substr;
                Permute(rest, answer + ch, model, index);
            }
        }
        private bool IsAnswerCorrect(GraphGeneratorModel model, string answer, int index)
        {
            //Utworzenie kopi macierzy na potrzeby validacji rozwiazan
            int[,] graphMatrixCopy = new int[model.GraphMatrix.GetLength(0), model.GraphMatrix.GetLength(0)];
            Array.Copy(model.GraphMatrix, graphMatrixCopy, graphMatrixCopy.Length);

            //1. podstawic answer za row[index] i sprawdzic czy suma stopni w kolumnach sie zgadza
            for (int k = 0; k < answer.Length; k++)
            {
                string a = answer[k].ToString();
                //ominiecie przekatnej
                if (k < index)
                {
                    graphMatrixCopy[index, k] = int.Parse(a);
                    graphMatrixCopy[k, index] = int.Parse(a);
                }

                if (k == index)
                {
                    graphMatrixCopy[index, k] = 0;
                    graphMatrixCopy[index, k + 1] = int.Parse(a);
                    graphMatrixCopy[k + 1, index] = int.Parse(a);
                }
                if (k > index)
                {
                    graphMatrixCopy[index, k + 1] = int.Parse(a);
                    graphMatrixCopy[k + 1, index] = int.Parse(a);
                }
            }
            return IsDegreeCorrect(model, graphMatrixCopy);
        }
        private bool IsDegreeCorrect(GraphGeneratorModel model, int[,] graphMatrixCopy)
        {
            int equalityCounter = 0;
            
            //sprawdzenie sumy stopnia w columnach
            var result = Enumerable.Range(0, graphMatrixCopy.GetLength(0))
                        .Select(c => Enumerable.Range(0, graphMatrixCopy.GetLength(1))
                        .Select(r => graphMatrixCopy[r, c]).Sum())
                        .ToList();

            result.Sort();
            result.Reverse();

            if (Enumerable.SequenceEqual(result, model.GraphSequenceCopy))
            {
                if (model.PermutationsInArray.Count > 0)
                {
                    for (int i = 0; i < model.PermutationsInArray.Count; i++)
                    {
                        var equal = model.PermutationsInArray[i].Rank == graphMatrixCopy.Rank &&
                        Enumerable.Range(0, model.PermutationsInArray[i].Rank).All(dimension => model.PermutationsInArray[i].GetLength(dimension) == graphMatrixCopy.GetLength(dimension)) &&
                        model.PermutationsInArray[i].Cast<int>().SequenceEqual(graphMatrixCopy.Cast<int>());

                        if (equal)
                            equalityCounter++;
                    }
                    if (equalityCounter == 0)
                        model.PermutationsInArray.Add(graphMatrixCopy);
                }
                else
                    model.PermutationsInArray.Add(graphMatrixCopy);
                return true;
            }
            else
                return false;
        }
        public Dictionary<string, int> AssignDegreesToNodes(List<int> graphSequence)
        {
            //przypisanie stopni do poszczegolnych wierzcholkow
            Dictionary<string, int> nodes = new Dictionary<string, int>();
            for (int i = 0; i < graphSequence.Count; i++)
            {
                nodes.Add("" + (i + 1), graphSequence[i]);
            }
            return nodes;
        }
        public void CreateGraph(int[,] graphMatrix, Dictionary<string, int> nodes)
        {
            var graph = new DotGraph("");

            for (int i = 0; i < nodes.Count; i++)
            {
                var key = nodes.Keys.ElementAt(i);
            }

            for (int i = 0; i < graphMatrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < graphMatrix.GetLength(1); j++)
                {
                    if (graphMatrix[i, j] == 1)
                    {
                        var edge = new DotEdge(nodes.Keys.ElementAt(i), nodes.Keys.ElementAt(j));
                        graph.Elements.Add(edge);
                    }
                }
            }
            var dot = graph.Compile();
            File.WriteAllText("C:\\Users\\eci\\Desktop\\graph.dot", dot);
        }
        public void CreateMatrixView(GraphGeneratorModel model, List<int> GraphSequence)
        {
            DataTable dt = new DataTable();
            int nbColumns = GraphSequence.Count;
            int nbRows = GraphSequence.Count;

            for (int i = 0; i < nbColumns; i++)
            {
                dt.Columns.Add((i + 1).ToString(), typeof(int));
            }
            if (model.PermutationsInArray.Count == 0)
            {
                for (int j = 0; j < nbRows; j++)
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < nbColumns; i++)
                    {
                        dr[i] = model.GraphMatrix[j, i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (int j = 0; j < nbRows; j++)
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < nbColumns; i++)
                    {
                        dr[i] = model.PermutationsInArray[model.counter][j, i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            model.MatrixView = dt.DefaultView;
        }
    }
}
