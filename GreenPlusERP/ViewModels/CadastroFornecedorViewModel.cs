using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenPlusERP.ViewModels
{
    public class CadastroFornecedorViewModel : viewModelBase
    {
        private FornecedorModel fornecedor;

        private IFornecedorRepository fornecedorRepository;

        public FornecedorModel Fornecedor
        {
            get { return fornecedor; }
            set { fornecedor = value; OnPropertyChanged(nameof(Fornecedor)); }
        }

        public ICommand CadastrarFornecedor {  get; set; }
        public ICommand ConsultarFornecedor {  get; set; }

        //construtor 
        public CadastroFornecedorViewModel()
        {
            fornecedorRepository = new FornecedorRepository();
            Fornecedor = new FornecedorModel();

            CadastrarFornecedor = new viewModelCommand(executeCadastro, canExecuteCadastro);
            ConsultarFornecedor = new viewModelCommand(executeConsulta, canExecuteConsulta);
        }

        private bool canExecuteConsulta(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Fornecedor.cnpj)) 
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
        

        private void executeConsulta(object obj)
        {
            string Cnpj = Regex.Replace(Fornecedor.cnpj, "[^0-9]+", "");
            Fornecedor = fornecedorRepository.cnpjByApi(Cnpj);
        }

        private bool canExecuteCadastro(object obj)
        {
            bool canExecute;

            if(string.IsNullOrWhiteSpace(Fornecedor.cnpj) ||
                string.IsNullOrWhiteSpace(Fornecedor.razaoSocial)||
                string.IsNullOrWhiteSpace(Fornecedor.situação) || 
                string.IsNullOrWhiteSpace(Fornecedor.email) || 
                string.IsNullOrWhiteSpace(Fornecedor.contato) || 
                string.IsNullOrWhiteSpace(Fornecedor.nomeResponsável) || 
                string.IsNullOrWhiteSpace(fornecedor.logradouro) || 
                string.IsNullOrWhiteSpace(Fornecedor.numero) ||
                string.IsNullOrWhiteSpace(Fornecedor.bairro) ||
                string.IsNullOrWhiteSpace(Fornecedor.cep) ||
                string.IsNullOrWhiteSpace(Fornecedor.uf) ||
                string.IsNullOrWhiteSpace(Fornecedor.municipio))
            {
                canExecute = false;
            }
            else
            {
                canExecute = true;
            }

            return canExecute;
        }

        private void executeCadastro(object obj)
        {
            fornecedorRepository.Add(Fornecedor);
            Fornecedor = new FornecedorModel();
        }



        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
