using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba_BinaryTree_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        BinaryTree<int> Tree = new BinaryTree<int>();
        private void TreeCreate_Click(object sender, RoutedEventArgs e)
        {
            TreeShow.Content="";
            Tree.Clear();
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                int value = rand.Next(40);
                Tree.Add(value);
            }
            foreach (int n in Tree)
                TreeShow.Content += Convert.ToString(n)+" ";
        }

        private void DeleteElem_Click(object sender, RoutedEventArgs e)
        {
            TreeShow.Content = "";
            Tree.Remove(Convert.ToInt32(TreeElem.Text));
            foreach (int n in Tree)
                TreeShow.Content += Convert.ToString(n) + " ";
        }

        private void AddElem_Click(object sender, RoutedEventArgs e)
        {
            TreeShow.Content = "";
            Tree.Add(Convert.ToInt32(TreeElem.Text));
            foreach (int n in Tree)
                TreeShow.Content += Convert.ToString(n) + " ";
        }

        private void SearchElem_Click(object sender, RoutedEventArgs e)
        {
            if (Tree.Contains(Convert.ToInt32(TreeElem.Text)))
                MessageBox.Show("Содержит");
            else
                MessageBox.Show("Не содержит");
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                TreeShow.Content = "";
                TreeShow.Content = string.Join("-",Tree.CLR());//прямой

            }
            if (e.Key == Key.F2)
            {
                TreeShow.Content = "";
                TreeShow.Content = string.Join("-", Tree.LCR());//обратный

            }
            if (e.Key == Key.F3)
            {
                TreeShow.Content = "";
                TreeShow.Content = string.Join("-", Tree.LRC());//симметричный

            }
            if (e.Key == Key.F4)
            {
                TreeShow.Content = "";
                Tree.Clear();

            }
        }
    }
}
