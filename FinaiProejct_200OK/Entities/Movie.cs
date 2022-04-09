using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    public class Movie : INotifyPropertyChanged
    {
        private int genreId;
        private int directorId;
        private string movieTitle;
        private DateTime releaseDate;
        [Key]
        public int MovieId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get
            {
                return this.genreId;
            }
            set
            {
                this.genreId = value;
                NotifiyPropertyChanged();
            }
        }
        [ForeignKey("Director")]
        public int DirectorId { get 
            {
                return this.directorId;
            } set 
            {
                this.directorId = value;
                NotifiyPropertyChanged(); 
            } }
        public string MovieTitle { get 
            {
                return movieTitle;
            } set {
                movieTitle = value;
                NotifiyPropertyChanged();
            } }
        public DateTime ReleaseDate { get
            {
                return releaseDate;
            }
            set
            {
                releaseDate = value;
                NotifiyPropertyChanged();
            } }

        public List<Review> Reviews { get; set; }

        public IMDBData imdbData { get; set; }

        public Movie( string movieTitle, DateTime releaseDate, int genreId, int directorId)
        {
            GenreId = genreId;
            DirectorId = directorId;
            MovieTitle = movieTitle;
            ReleaseDate = releaseDate;
        }

        public Movie() { }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifiyPropertyChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
