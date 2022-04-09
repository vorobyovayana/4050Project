using FinaiProejct_200OK.Entities;
using FinaiProejct_200OK.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Login loginPage;
        Button subLogInButton;
        CreateAccount createPage;
        List<MovieTempForList> tempMovieList;
        DataGrid movieGrid;
        List<Director> directors = new List<Director>();
        List<Genre> genres = new List<Genre>();
        List<Movie> movies;
        User u = null;
        FileService fs = new FileService();
        DirectorParser dp = new DirectorParser();
        GenreParser gp = new GenreParser();
        string directorFileName = "";
        string genreFileName = "";

        public MainPage()
        {
            InitializeComponent();
            commonUsage();
        }

        public MainPage(User user)
        {
            InitializeComponent();
            u =user;

            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            /*parentWindow.LogoutButton.Visibility = Visibility.Visible;
            parentWindow.LoginButton.Visibility = Visibility.Hidden;*/
            Great.Content = "Hi, " + u.UserName;
            commonUsage();
        }

        public void commonUsage() {
            movies = new List<Movie>();
            //myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;
            createPage = new CreateAccount();
            ReadDataToDatabase();
            /*InitializeMovieGrid();*/
            PopulateMovie();
            InitializeDirectorsListBox();
            InitializeGenresListBox();
            PopulateMovie();
            movieGrid = MovieDataGrid;

            toggleEvent(true);
        }
        private void SearchInput(Object o, EventArgs e)
        {

            string input = SearchTextBox.Text;
            using (var ctx = new MovieContext())
            {
                movies = ctx.Movie.ToList();
                List<MovieTempForList> tempMovieListFull = new List<MovieTempForList>();
                foreach (Movie m in movies)
                {
                    MovieTempForList currentMovie = new MovieTempForList();
                    currentMovie.MovieId = m.MovieId;
                    currentMovie.MovieTitle = m.MovieTitle;
                    currentMovie.ReleaseDate = m.ReleaseDate.ToShortDateString();
                    currentMovie.MovieDirector = ctx.Director.Where(x => x.DirectorId == m.DirectorId).First().ToString();
                    currentMovie.MovieGenres = ctx.Genre.Where(x => x.GenreId == m.GenreId).First().ToString();
                    tempMovieListFull.Add(currentMovie);
                }
                tempMovieList = tempMovieListFull.Where(x => x.MovieDirector.Contains(input) || x.MovieGenres.Contains(input) || x.MovieTitle.Contains(input)
                || x.MovieId.ToString().Contains(input) || x.ReleaseDate.ToString().Contains(input))
                    .ToList();
            }
            MovieDataGrid.ItemsSource = tempMovieList;
        }
        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                //subLogInButton.Click += SubLoginButtonClick;
                //CreateButton.Click += CreateButtonClick;
                //createPage.SubCreateButton.Click += SubCreateButtonClick;

                AddGenreBtn.Click += selectGenreFile;
                AddDirectorBtn.Click += selectDirectorFile;
                MovieDataGrid.MouseDoubleClick += DataGridCellMouseDoubleClick;
            }
        }
        
        private void DataGridCellMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Movie temp = new Movie();
            var cellInfo = MovieDataGrid.SelectedCells[0];
            var id = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            temp.MovieId = Convert.ToInt32(id);
            cellInfo = MovieDataGrid.SelectedCells[1];
            var name = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            temp.MovieTitle = name;
            this.NavigationService.Navigate(new Detail(temp, u));

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
            if (o.Equals(AddGenreBtn))
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
            using (var ctx = new MovieContext())
            {
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

       /* private void SubLoginButtonClick(Object o, EventArgs e)
        {
            using (var ctx = new MovieContext())
            {
                var user = ctx.User.Where(x => x.UserName == loginPage.UserNameTextBox.Text).First();

                if (loginPage.PasswordTextBox.Text == user.getPassword().Trim())
                {
                    myUser = user;*//*
                    LogoutButton.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Hidden;*//*
                    loginPage.HintTextBlock.Text = "";
                    loginPage.UserNameTextBox.Text = "";
                    loginPage.PasswordTextBox.Text = "";
                    loginPage.Hide();
                    CreateButton.Visibility = Visibility.Hidden;
                    AccountTextBlock.Text = "Hello, " + myUser.UserName;
                    AccountTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    loginPage.HintTextBlock.Visibility = Visibility.Visible;
                    loginPage.HintTextBlock.Text = "Wrong input information";
                }
            }
        }*/



        private void PopulateMovie()
        {
            //MovieDataGrid.Items.Clear();
            using (var ctx = new MovieContext())
            {
                movies = ctx.Movie.ToList();
                List<MovieTempForList> tempMovieList = new List<MovieTempForList>();
                foreach (Movie m in movies)
                {
                    MovieTempForList currentMovie = new MovieTempForList();
                    currentMovie.MovieId = m.MovieId;
                    currentMovie.MovieTitle = m.MovieTitle;
                    currentMovie.ReleaseDate = m.ReleaseDate.ToShortDateString(); 
                    currentMovie.MovieDirector = ctx.Director.Where(x => x.DirectorId == m.DirectorId).First().ToString();
                    currentMovie.MovieGenres = ctx.Genre.Where(x => x.GenreId == m.GenreId).First().ToString();
                    tempMovieList.Add(currentMovie);
                }
                MovieDataGrid.ItemsSource = tempMovieList;
            }
        }

        private void InitializeMovieGrid()
        {
            DataGridTextColumn movieTitleColumn = new DataGridTextColumn();
            movieTitleColumn.Header = "Movie Title";
            movieTitleColumn.Binding = new Binding("MovieTitle");

            DataGridTextColumn movieReleaseDateColumn = new DataGridTextColumn();
            movieReleaseDateColumn.Header = "Release Date";
            movieReleaseDateColumn.Binding = new Binding("ReleaseDate");
            movieReleaseDateColumn.Binding.StringFormat = "dd/MM/yyyy";


            DataGridTextColumn genreColumn = new DataGridTextColumn();
            genreColumn.Header = "Genre";

            DataGridTextColumn directorColumn = new DataGridTextColumn();
            directorColumn.Header = "Director";

            MovieDataGrid.Columns.Add(movieTitleColumn);
            MovieDataGrid.Columns.Add(movieReleaseDateColumn);
            MovieDataGrid.Columns.Add(genreColumn);
            MovieDataGrid.Columns.Add(directorColumn);
        }
    public class MovieTempForList
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string ReleaseDate { get; set; }
        public string MovieDirector { get; set; }
        public string MovieGenres { get; set; }
    }
    private void ReadDataToDatabase()
        {

            using (var ctx = new MovieContext())
            {
                // First from director                
                var count = ctx.Director.Count();
                if (count < 5)
                {
                    List<Director> directorList = new List<Director>();
                    directorList = DirectorParser.ParseDirector(fs.ReadFile(@"..\\..\\Data\\directors.csv"));
                    foreach (Director director in directorList)
                    {
                        ctx.Director.Add(director);
                    }
                }

                ctx.SaveChanges();
                // Second is Genre                
                count = ctx.Genre.Count();
                if (count < 5)
                {
                    List<Genre> genreList = new List<Genre>();
                    genreList = GenreParser.ParseGenre(fs.ReadFile(@"..\\..\\Data\\genres.csv"));
                    foreach (Genre genre in genreList)
                    {
                        ctx.Genre.Add(genre);
                    }
                }

                ctx.SaveChanges();

                // Then add into Movie
                count = ctx.Movie.Count();
                if (count < 5)
                {
                    List<Movie> movieList = new List<Movie>();
                    movieList = MovieParser.ParseMovie(fs.ReadFile(@"..\\..\\Data\\movies.csv"));
                    for (int i = 0; i < 5; i++)
                    {
                        List<Genre> myGenres = new List<Genre>();
                        myGenres.Add(ctx.Genre.ToList()[i]);
                        myGenres.Add(ctx.Genre.ToList()[(i + 2) % 5]);
                        // movieList[i].Genres = myGenres;                        
                        //movieList[i].MovieDirector = ctx.Director.ToList()[i];
                        ctx.Movie.Add(movieList[i]);

                    }
                }

                ctx.SaveChanges();
            }
        }


    }
}

