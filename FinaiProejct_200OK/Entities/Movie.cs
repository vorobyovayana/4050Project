using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    class Movie
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual List<Genre> Genres { get; set; }
        public virtual Detail MovieDetail { get; set; }
        public virtual Director MovieDirector { get; set; }
    }
}
