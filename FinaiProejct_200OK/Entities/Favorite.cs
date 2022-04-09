using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    public class Favorite
    {
        [Key]
        [Required]
        public int MemberId { get; set; }
        [Key]
        [Required]
        public int MovieId { get; set; }

        public Favorite(int memberId, int movieId)
        {
            MemberId = memberId;
            MovieId = movieId;
        }
        public Favorite() { }
    }
}
