using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<Review> Reviews { get; set; }

        public virtual List<Genre> Genres { get; set; }
        public virtual Detail MovieDetail { get; set; }
        public virtual Director MovieDirector { get; set; }

        public Movie(int movieId, string movieTitle, DateTime releaseDate)
        {
            MovieId = movieId;
            MovieTitle = movieTitle;
            ReleaseDate = releaseDate;
        }
    }
}
