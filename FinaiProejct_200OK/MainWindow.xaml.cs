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
using System.Windows.Forms;
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
        List<Movie> movies = new List<Movie>();
        FileService fs = new FileService();
        DirectorParser dp = new DirectorParser();
        GenreParser gp = new GenreParser();
        MovieCSVParser mp = new MovieCSVParser();

        public MainWindow()
        {
            InitializeComponent();
            myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;

            InitializeDirectorsListBox();
            InitializeGenresListBox();
            InitializeMovieDataGrid();
            movieGrid = MovieDataGrid;
            toggleEvent(true);

            
            
        }
        private void InitializeMovieDataGrid()
        {
            MovieDataGrid.IsReadOnly = true;
            MovieDataGrid.ItemsSource = movies;
            DataGridTextColumn TitleColumn = new DataGridTextColumn();
            TitleColumn.Header = "Movie Title";
            TitleColumn.Binding = new Binding("MovieTitle");

            DataGridTextColumn ReleaseDateCol = new DataGridTextColumn();
            ReleaseDateCol.Header = "Release Date";
            ReleaseDateCol.Binding = new Binding("ReleaseDate");

            MovieDataGrid.Columns.Add(TitleColumn);
            MovieDataGrid.Columns.Add(ReleaseDateCol);
        }
        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                fileDialogueButton.Click += selectFile;
                LoginButton.Click += LoginButtonClick;
                subLogInButton.Click += SubLoginButtonClick;
                LogoutButton.Click += LogOutButtonClick;
                
            }
        }

        private void selectFile(object o, EventArgs e)
        {
            OpenFileDialog openFileDialogue = new OpenFileDialog();
            openFileDialogue.InitialDirectory = "c:\\temp";
            openFileDialogue.Filter = "CSV Files (*.csv)|*.csv|PSV Files(*.psv)|*.psv";
            openFileDialogue.RestoreDirectory = true;

            Nullable<bool> result = openFileDialogue.ShowDialog();
            var fileName = "";

            if (result == true)
            {
                //As soon as there is a result from showdialogue assign it to the the fileName 
                fileName = openFileDialogue.FileName;
            }


            //Listfrom file
            movies = mp.parseRoster(fs.ReadFile(fileName));
            //Read the fileContents in from the parser

            allCourseList.AddRange(loadCourseList);
            initializeCourseList();
            initializeDataGrid();
            initializeRoomListBox();
            initializeDepartmentListBox();
            userMessageBox.Text = "Added " + loadCourseList.Count() + " courses to the list.";

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
