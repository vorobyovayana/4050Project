using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int MovieId { get; set; }
        public string ReviewDesc { get; set; }

        public Review(int reviewId, int movieId, string reviewDesc)
        {
            ReviewId = reviewId;
            MovieId = movieId;
            ReviewDesc = reviewDesc;
        }
        public Review() { }
    }
}
