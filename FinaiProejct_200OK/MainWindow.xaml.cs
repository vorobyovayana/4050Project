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

        string directorFileName = "";
        string genreFileName = "";

        public MainWindow()
        {
            
            InitializeComponent();
            movies = new List<Movie>();
            myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;
            createPage = new CreateAccount();
            ReadTheUser();

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
                //CreateButton.Click += CreateButtonClick;
                //createPage.SubCreateButton.Click += SubCreateButtonClick;
                
                AddGenreBtn.Click += selectGenreFile;
                AddDirectorsBtn.Click += selectDirectorFile;

            }
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {            
            loginPage.Show();
        }

        private void selectDirectorFile(object o, EventArgs e)
        {
            if (o.Equals(AddDirectorsBtn))
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
                directors.AddRange(loadDirectorsList);

                InitializeDirectorsListBox();
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
                InitializeGenresListBox();
            }
        }

        public void InitializeDirectorsListBox()
        {
            DirectorListBox.Items.Clear();
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
              
                if (loginPage.PasswordTextBox.Text == user.getPassword().Trim())
                {
                    myUser = user;
                    LogoutButton.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Hidden;
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
        }

        private void LogOutButtonClick(Object o, EventArgs e)
        {
                    
            myUser = null;
            LogoutButton.Visibility = Visibility.Hidden;
            LoginButton.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Visible;
            AccountTextBlock.Text = "";
            AccountTextBlock.Visibility = Visibility.Hidden;
            
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

        private void ReadTheUser()
        {
            using (var ctx = new MovieContext())
            {
                var count = ctx.User.Count();
                if (count == 0)
                {
                    List<User> usersList = new List<User>();
                    usersList = UserParser.ParseUser(fs.ReadFile(@"..\\..\\Data\\user.csv"));
                    foreach (User theUser in usersList)
                    {
                        ctx.User.Add(theUser);
                    }
                    ctx.SaveChanges();
                }
            }
        }

        

        
    }
}
