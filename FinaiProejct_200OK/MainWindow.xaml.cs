using FinaiProejct_200OK.Entities;
using FinaiProejct_200OK.Utilities;
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

namespace FinaiProejct_200OK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataGrid movieGrid;
        List<Movie> movies;
        public MainWindow()
        {
            InitializeComponent();

            movieGrid = MovieDataGrid;
            

            string moviePath = @"..\..\Data\movies.csv";
            string content = FileService.ReadFile(moviePath);            

            movies = MovieParser.parseRoaster(content);

             

            PopulateMovieData();



            
            

            
            
        }

        private void PopulateMovieData()
        {
            movieGrid.SelectionMode = DataGridSelectionMode.Single;
            movieGrid.IsReadOnly = true;

            movieGrid.ItemsSource = movies;
        }
    }
}
