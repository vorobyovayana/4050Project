using FinaiProejct_200OK.Entities;
using FinaiProejct_200OK.Utilities;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            isFavorite();
            AddReview.Click += addReviewEvent;
        }

        private void isFavorite()
        {
            using (var ctx = new MovieContext())
            {
                var favorite = ctx.Favorite.Where(x => x.MovieId == movie.MovieId && x.MemberId==user.UserId).FirstOrDefault();
                if (favorite == null) 
                { 
                    
                }
            }
        }

        private void HandleLinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink hl = (Hyperlink)sender;
            string navigateUri = hl.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }


        private void addReviewEvent(object o, EventArgs e)
        {
            if(user != null) { 
                using (var ctx = new MovieContext())
                {
                    Review temp = new Review();
                    temp.ReviewDesc = reviewDesc.Text;
                    temp.MovieId = movie.MovieId;
                    temp.UserId = user.UserId;
                    ctx.Review.Add(temp);
                    ctx.SaveChanges();
                    ctx.Review.Add(temp);
                    getData();
                }
            }
            else
            {
                MessageBox.Show("Please login first");
            }
        }

        public void getData() {
            using (var ctx = new MovieContext())
            {
                var IMDBData = ctx.IMDBData.Where(x => x.MovieId == movie.MovieId).FirstOrDefault() ;
                displayIMDB(IMDBData);

                var review = ctx.Review.Where(x => x.MovieId == movie.MovieId).ToList<Review>();
                displayReview(review);
            }
        }
        public void displayIMDB(IMDBData iMDBData) {

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@iMDBData.posterPath, UriKind.Absolute);
            bitmap.EndInit();
            MoiveImg.Source = bitmap;
            MovieName.Text = movie.MovieTitle;
            Description.Text = movie.MovieDescription;
            this.DataContext = iMDBData.imdbPath;
        }
        public void displayReview(List<Review> reviewList)
        {
            stackPanel.Children.Clear();
            if (reviewList.Count == 0) {
                TextBlock reviewContent = new TextBlock();
                reviewContent.Text = "No reviews. You're welcome to add one";
                stackPanel.Children.Add(reviewContent);
            }
            using (var ctx = new MovieContext()) { 
                foreach (Review r in reviewList)
                {
                    var user = ctx.User.Where(x => x.UserId == r.UserId).FirstOrDefault() ;
                    StackPanel reviewItem = new StackPanel();
                    TextBlock reviewContent = new TextBlock();
                    TextBlock userName = new TextBlock();
                    userName.Text = user.UserName +" said:   ";
                    reviewContent.Text = r.ReviewDesc;
                    reviewItem.Children.Add(userName);
                    reviewItem.Children.Add(reviewContent);
                    reviewItem.Background = new SolidColorBrush(Colors.Blue);
                    reviewItem.Orientation = Orientation.Horizontal;
                    reviewItem.Width = 299;
                    reviewItem.Height = 50;
                    stackPanel.Children.Add(reviewItem);
                    stackPanel.Visibility = Visibility.Visible;
                    //reviewGrid.Children.Add(reviewItem);
                }
            }
        }

    }
}
