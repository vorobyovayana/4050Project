using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Entities
{
    class IMDBData
    {
        
        [Key]
        public int MovieId { get; set; }
        public string imdbPath { get; set; }
    }
}
