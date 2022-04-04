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
        System.Windows.Controls.Button subLogInButton;

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
            myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;

            InitializeDirectorsListBox();
            InitializeGenresListBox();
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
                AddGenresBtn.Click += selectGenreFile;
                AddDirectorBtn.Click += selectDirectorFile;

            }
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {            
            loginPage.Show();
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
                directors.AddRange(loadDirectorsList);

                InitializeDirectorsListBox();
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
                /*var user = ctx.User.Where(x => x.UserName == loginPage.UserNameTextBox.Text).First();
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
                }*/
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

    }
}
