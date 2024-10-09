using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    [Table("Plantio")]
    public class PlantioModel
    {
        [Key]
        public int plantioId {  get; set; }
        public long lote {  get; set; }
        public int PlantaId { get; set; }
        public string unidadeMedida { get; set; }
        public long quantidade { get; set; }
        public DateTime dataPlantio { get; set; }
        public DateTime previsaoColheita { get; set; }
        public string localizacao { get; set; }

        [ForeignKey("PlantaId")]
        public ProductModel produto { get; set; }
    }
}
