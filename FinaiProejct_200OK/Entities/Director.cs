using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    class Director
    {
        [Key]
        public int DirectorId { get; set; }

        public string DirectorName { get; set; }
        public List<Movie> Movies { get; set; }

        public Director()
        {

        }

        public override string ToString()
        {
            return DirectorName;
        }
    }
}
