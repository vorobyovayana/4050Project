using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<Review> Reviews { get; set; }

        public IMDBData imdbData { get; set; }

        public Movie( string movieTitle, DateTime releaseDate, int genreId, int directorId,string movieDescription)
        {
            GenreId = genreId;
            DirectorId = directorId;
            MovieTitle = movieTitle;
            ReleaseDate = releaseDate;
            MovieDescription = movieDescription;

        }

        public Movie() { }
    }
}
