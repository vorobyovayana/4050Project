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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            HintTextBlock.Visibility = Visibility.Hidden;
            toggleEvent();
        }

        private void toggleEvent()
        {
            Back.Click += navigateBackButton_Click;
            SubLoginButton.Click += checkPwd;
        }

        private void checkPwd(object sender, RoutedEventArgs e)
        {
            User user = null;
            using (var ctx = new MovieContext())
            {
                user = ctx.User.Where(x => x.UserName == UserNameTextBox.Text).First();

                if (PasswordTextBox.Text == user.getPassword().Trim())
                {
                    HintTextBlock.Text = "";
                    UserNameTextBox.Text = "";
                    PasswordTextBox.Text = "";
                }
                else
                {
                    HintTextBlock.Visibility = Visibility.Visible;
                    HintTextBlock.Text = "Wrong input information";
                }
            }
            this.NavigationService.Navigate(new MainPage(user));
        }
        
        void navigateBackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back one page from this page, if there is an entry
            // in back navigation history
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No entries in back navigation history.");
            }
        }
    }
}
