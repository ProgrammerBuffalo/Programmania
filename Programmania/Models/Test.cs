using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    public class Test
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }
        
        public string Answer3 { get; set; }
        
        public string Answer4 { get; set; }

        public string Question { get; set; }

        [Range(1, 4)]
        public int Correct { get; set; }

    }
}
