using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    [Table("UsersDisciplines")]
    public class UserDiscipline
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DisciplineId { get; set; }

        public User User { get; set; }

        public Discipline Discipline { get; set; }

        public int LessonOrder { get; set; }
    }
}
