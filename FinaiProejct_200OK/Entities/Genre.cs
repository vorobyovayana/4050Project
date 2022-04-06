using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
       /* public List<Movie> MovieInThisGenre { get; set; }*/

        public Genre()
        {

        }

        public override string ToString()
        {
            return GenreName;
        }
    }
}
