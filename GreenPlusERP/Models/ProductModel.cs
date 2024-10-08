using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    [Table("Produto")]
    public class ProductModel
    {
        [Key]
        public int PlantaId {  get; set; }
        public string NomePlanta { get; set; }
        public string NomeCientifico { get; set; }
        public string Classificacao { get; set; }
        public int TempoEstimado { get; set; }
        public int Temperatura { get; set; }
        public int Irrigacao { get; set; }
        public double ValorVenda { get; set; }
    }
}
