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
    ///    
    public partial class MainWindow : Window
    {

        private User myUser;
        Login loginPage;
        Button subLogInButton;
        CreateAccount createPage;

        DataGrid movieGrid;
        List<Director> directors = new List<Director>();
        List<Genre> genres = new List<Genre>();
        List<Movie> movies;

        FileService fs = new FileService();
        DirectorParser dp = new DirectorParser();
        GenreParser gp = new GenreParser();

        public MainWindow()
        {
            
            InitializeComponent();
            movies = new List<Movie>();
            myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;
            createPage = new CreateAccount();

            InitializeDirectorsListBox();
            InitializeGenresListBox();
            PopulateMovie();
            movieGrid = MovieDataGrid;

            toggleEvent(true);

            
            
        }

        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                LoginButton.Click += LoginButtonClick;
                subLogInButton.Click += SubLoginButtonClick;
                LogoutButton.Click += LogOutButtonClick;
                CreateButton.Click += CreateButtonClick;
                createPage.SubCreateButton.Click += SubCreateButtonClick;
                
            }
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {            
            loginPage.Show();
        }
            
        

        public void InitializeDirectorsListBox()
        {
            DirectorListBox.Items.Clear();
            List<Director> loadDirectorsList = DirectorParser.ParseDirector(fs.ReadFile(@"..\\..\\Data\\directors.csv"));
            //Read the fileContents in from the parser

            directors.AddRange(loadDirectorsList);
            
            var directorsList = directors.Select(x => x.DirectorName).Distinct();

            foreach (var c in directorsList)
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

        public void InitializeGenresListBox()
        {
            GenreListBox.Items.Clear();
            List<Genre> loadGenresList = GenreParser.ParseGenre(fs.ReadFile(@"..\\..\\Data\\genres.csv"));
            //Read the fileContents in from the parser
            genres.AddRange(loadGenresList);

            var genresList = genres.Select(x => x.GenreName).Distinct();

            foreach (var c in genresList)
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

        private void SubLoginButtonClick(Object o, EventArgs e)
        {
            using (var ctx = new MovieContext())
            {
                var user = ctx.User.Where(x => x.UserName == loginPage.UserNameTextBox.Text).First();
                if (loginPage.PasswordTextBox.Text == user.getPassword())
                {
                    myUser = user;
                    LogoutButton.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Hidden;
                    loginPage.Close();
                }
                else
                {
                    loginPage.HintTextBlock.Visibility = Visibility.Visible;
                    loginPage.HintTextBlock.Text = "Wrong input information";
                }
                LogoutButton.Visibility = Visibility.Visible;
                LoginButton.Visibility = Visibility.Hidden;
                loginPage.HintTextBlock.Text = "";
                loginPage.UserNameTextBox.Text = "";
                loginPage.PasswordTextBox.Text = "";
                loginPage.Hide();
            }
        }

        private void LogOutButtonClick(Object o, EventArgs e)
        {
                    
            myUser = null;
            LogoutButton.Visibility = Visibility.Hidden;
            LoginButton.Visibility = Visibility.Visible;
            
        }

        private void CreateButtonClick(Object o, EventArgs e)
        {
            createPage.Show();
        }

        private void SubCreateButtonClick(Object o, EventArgs e)
        {
            if (createPage.CreateUserNameTextBox.Text.Length == 0 || createPage.CreatePasswordTextBox.Text.Length == 0)
            {
                createPage.CreateHintTextBlock.Text = "Please input both user name and password";
            } else
            {
                using (var ctx = new MovieContext())
                {
                    var count = ctx.User.Where(x => x.UserName == createPage.CreateUserNameTextBox.Text).Count();

                    if (count == 0)
                    {
                        User newUser = new User();
                        newUser.UserName = createPage.CreateUserNameTextBox.Text;
                        newUser.setPassword(createPage.CreatePasswordTextBox.Text);
                        ctx.User.Add(newUser);
                        ctx.SaveChanges();
                        createPage.Hide();

                    }
                    else
                    {
                        createPage.CreateHintTextBlock.Text = "The user name is used. Please input another one!";
                    }
                }
            }
            
        }

        private void PopulateMovie()
        {
            MovieDataGrid.Items.Clear();
            movies = MovieParser.ParseMovie(fs.ReadFile(@"..\\..\\Data\\movies.csv"));
            

            foreach (Movie m in movies)
            {
                MovieDataGrid.Items.Add(m);
                
            }
        }

        

        
    }
}
