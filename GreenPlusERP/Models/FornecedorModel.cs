using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Models
{
    [Table("Fornecedor")]
    public class FornecedorModel
    {
        [Key]
        public int fornecId { get; set; }
        public string cnpj {  get; set; }
        public string razaoSocial {  get; set; }
        public string situacao {  get; set; }
        public string email {  get; set; }
        public string contato {  get; set; }
        public string nomeResponsavel {  get; set; }
        public string logradouro {  get; set; }
        public string numero {  get; set; }
        public string bairro {  get; set; }
        public string complemento {  get; set; }
        public string cep {  get; set; }
        public string uf {  get; set; }
        public string municipio {  get; set; }
    }
}
