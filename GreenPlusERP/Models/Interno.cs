using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    [Table("Interno")]
    public class Interno
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
    }
}
