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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {

        
        public Login()
        {
            InitializeComponent();

            HintTextBlock.Visibility = Visibility.Hidden;


        }

        private void LoginClick(Object o, EventArgs e)
        {
            using (var ctx = new MovieContext())
            {
                var user = ctx.User.Where(x => x.UserName == UserNameTextBox.Text).First();
                if (PasswordTextBox.Text == user.getPassword())
                {
                    
                    this.Close();
                } else
                {
                    HintTextBlock.Visibility = Visibility.Visible;
                    HintTextBlock.Text = "Wrong input information";
                }
            }
        }
    }
}
