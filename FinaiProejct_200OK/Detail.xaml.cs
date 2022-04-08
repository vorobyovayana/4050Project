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
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Page
    {
        Movie movie;
        User user;
        public Detail(){ InitializeComponent();  }
        public Detail(Movie m, User u)
        {
            InitializeComponent();
            movie = m;
            user = u;
            getData();
        }

        public void getData() {
            using (var ctx = new MovieContext())
            {
                var IMDBData = ctx.IMDBData.Where(x => x.MovieId == movie.MovieId).First();
                displayIMDB(IMDBData);

            }
        }
        public void displayIMDB(IMDBData iMDBData) {

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@iMDBData.imdbPath, UriKind.Absolute);
            bitmap.EndInit();
            MoiveImg.Source = bitmap;
            MovieName.Text = movie.MovieTitle;

        }
    }

}
