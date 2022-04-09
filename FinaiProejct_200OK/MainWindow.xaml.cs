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

        MainWindow mainWindow;
        string userName = string.Empty;
        public MainWindow()
        {

            InitializeComponent();
            mainWindow = Window.GetWindow(this) as MainWindow;
            Frame frame = (Frame)mainWindow.FindName("frame");
            frame.Navigate(new MainPage());
            toggleEvent(true);

        }

        private void toggleEvent(bool toggle)
        {
            if (toggle)
            {
                LoginButton.Click += LoginButtonClick;
                LogoutButton.Click += LogOutButtonClick;
                CreateButton.Click += CreateButtonClick;
                FavoritesButton.Click += GoToFavList;
            }
        }
        private void GoToFavList(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new CreateAccount());
        }
        private void toggleVisible(bool toggle)
        {
            if (toggle)
            {
                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Visible;
                CreateButton.Visibility = Visibility.Visible;
            }
            else {
                LoginButton.Visibility = Visibility.Hidden;
                LogoutButton.Visibility = Visibility.Hidden;
                CreateButton.Visibility = Visibility.Hidden;
            }
        }

        

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {

            toggleVisible(false);
            frame.Navigate(new CreateAccount());
        }

        private void LoginButtonClick(Object o, EventArgs e)
        {
            
            toggleVisible(false);
            frame.Navigate(new LoginPage());
        }
        private void LogOutButtonClick(Object o, EventArgs e)
        {

            toggleVisible(true);
            frame.Navigate(new MainPage());

        }
    }
}