using FinaiProejct_200OK.Entities;
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
        
        
        public MainWindow()
        {
            InitializeComponent();
            myUser = null;

            toggleEvent(true);

            
            
        }

        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                LoginButton.Click += LoginButtonClick;
            }
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {
            Login loginPage = new Login();
            loginPage.Show();
        }
    }
}
