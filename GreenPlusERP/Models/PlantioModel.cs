using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    public class PlantioModel
    {
        public long lote {  get; set; }
        public ProductModel Product { get; set; }
        public string unidadeMedida { get; set; }
        public long quantidade { get; set; }
        public DateTime dataPlantio { get; set; }
        public DateTime previsaoColheita { get; set; }
    }
}
