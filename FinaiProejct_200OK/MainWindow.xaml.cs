using FinaiProejct_200OK.Entities;
using FinaiProejct_200OK.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        List<Director> directors = new List<Director>();
        List<Genre> genres = new List<Genre>();

        FileService fs = new FileService();
        DirectorParser dp = new DirectorParser();
        GenreParser gp = new GenreParser();

        string directorFileName = "";
        string genreFileName = "";

        public MainWindow()
        {
            InitializeComponent();
            movieGrid = MovieDataGrid;
        }

        private void selectDirectorFile(object o, EventArgs e)
        {
            if (o.Equals(AddDirectorBtn))
            {
                OpenFileDialog openFileDialogue = new OpenFileDialog();
                // Only show user the Data folder with only "directors.csv" file in it
                string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Data");
                openFileDialogue.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
                openFileDialogue.Filter = "CSV Files(directors.csv)|directors.csv";
                openFileDialogue.RestoreDirectory = true;

                Nullable<bool> result = openFileDialogue.ShowDialog();

                if (result == true)
                {
                    directorFileName = openFileDialogue.FileName;
                }
                else
                {
                    return;
                }
                List<Director> loadDirectorsList = DirectorParser.ParseDirector(fs.ReadFile(directorFileName));
                uploadDirectors();
            }
        }

        private void selectGenreFile(object o, EventArgs e)
        {
            if (o.Equals(AddGenresBtn))
            {
                OpenFileDialog openFileDialogue = new OpenFileDialog();
                // Only show user the Data folder with only "directors.csv" file in it
                string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Data");
                openFileDialogue.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
                openFileDialogue.Filter = "CSV Files(genres.csv)|genres.csv";
                openFileDialogue.RestoreDirectory = true;

                Nullable<bool> result = openFileDialogue.ShowDialog();
                if (result == true)
                {
                    genreFileName = openFileDialogue.FileName;
                }
                else
                {
                    return;
                }
                List<Genre> loadGenreList = GenreParser.ParseGenre(fs.ReadFile(genreFileName));
                genres.AddRange(loadGenreList);
                uploadGenres();

            }
        }

        public void uploadGenres()
        {
            using (var ctx = new MovieContext())
            {
                foreach (Genre g in genres)
                {
                    ctx.Genre.Add(g);
                    ctx.SaveChanges();
                }

            }
            InitializeGenresListBox();
        }

        public void uploadDirectors()
        {
            using (var ctx = new MovieContext())
            {
                foreach (Director d in directors)
                {
                    ctx.Director.Add(d);
                    ctx.SaveChanges();
                }

            }
            InitializeDirectorsListBox();

        }

        public void InitializeDirectorsListBox()
        {
            DirectorListBox.Items.Clear();
            using (var ctx = new MovieContext())
            {
                var directorDB = ctx.Director.ToList().Distinct();
                foreach (var c in directorDB)
                {
                    try
                    {
                        ListViewItem l = new ListViewItem();
                        l.Content = c;
                        DirectorListBox.Items.Add(l);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        public void InitializeGenresListBox()
        {
            GenreListBox.Items.Clear();
            using (var ctx = new MovieContext()) {
                var genreDB = ctx.Genre.ToList().Distinct();
                foreach (var c in genreDB)
                {
                    try
                    {
                        ListViewItem l = new ListViewItem();
                        l.Content = c;
                        GenreListBox.Items.Add(l);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

            }
            
        }
    }
}
