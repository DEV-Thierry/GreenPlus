using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    public class ProductModel
    {
        public string id {  get; set; }
        public string nome { get; set; }
        public string nomeCientifico { get; set; }
        public string classificacao { get; set; }
        public string tempoEstimado { get; set; }
        public string temperatura { get; set; }
        public string irrigacao { get; set; }
        public float valorVenda { get; set; }
    }
}
