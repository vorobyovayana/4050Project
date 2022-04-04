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
    public partial class MainWindow : Window
    {
        private User myUser;
        Login loginPage;
        System.Windows.Controls.Button subLogInButton;
        
        
        public MainWindow()
        {
            InitializeComponent();
            myUser = null;
            loginPage = new Login();
            subLogInButton = loginPage.SubLoginButton;

            toggleEvent(true);

            
            
        }

        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                LoginButton.Click += LoginButtonClick;
                subLogInButton.Click += SubLoginButtonClick;
                LogoutButton.Click += LogOutButtonClick;
                
            }
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {            
            loginPage.Show();
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
