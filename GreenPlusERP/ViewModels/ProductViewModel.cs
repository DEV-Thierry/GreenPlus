using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GreenPlusERP.ViewModels
{
    public class ProductViewModel : viewModelBase
    {
        //fields
        private ProductModel _product;
        IProductRepository _repository;
        private bool isDetalheVisible;

        public ProductModel Product 
        {
            get { return _product; } 
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        public bool IsDetalheVisible
        {
            get { return isDetalheVisible; }
            set { isDetalheVisible = value; OnPropertyChanged(nameof(IsDetalheVisible)); }
        }

        //commands
        public ICommand CadastroCommand {  get; set; }
        public ICommand ConsultarCommand { get; set; }
        public ICommand DeletarCommand { get; set; }

        //constructor
        public ProductViewModel()
        {
            _repository = new productRepository();
            Product = new ProductModel();
            CadastroCommand = new viewModelCommand(ExecuteCadastro, CanExecuteCadastro);
            ConsultarCommand = new viewModelCommand(ExecuteConsulta, CanExecuteConsulta);
            DeletarCommand = new viewModelCommand(ExecuteDelete, CanExecuteDelete);
            Product.valorVenda = 0;
            isDetalheVisible = true;
        }

        private bool CanExecuteDelete(object obj)
        {
            if(string.IsNullOrWhiteSpace(Product.nomeCientifico))
            {
                return false;             }
            else
            {
                if(_repository.ExistingData(Product))
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }
        

        private void ExecuteDelete(object obj)
        {
            string confirma = "Tem certeza que deseja deletar o produto: \"" + Product.nome.ToString() + "\"";
            string caption = "Deletando dados";
            var result = MessageBox.Show(confirma, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _repository.Remove(Product.nomeCientifico);
                MessageBox.Show($"prduto: {Product.nome} deletado com sucesso!");
                Product = new ProductModel();
            }
        }

        private bool CanExecuteConsulta(object obj)
        {
            if(string.IsNullOrWhiteSpace(Product.nome))
            {
                return false;
            }
            else
            {
                return true;
            } 
                
        }

        private void ExecuteConsulta(object obj)
        {
            try
            {
                
                Product = _repository.GetByName(Product);
            }catch (Exception ex)
            {
                MessageBox.Show("Falha ao realizar a consulta " + ex.Message);
            }
        }


        //methods
        private bool CanExecuteCadastro(object obj)
        {
            bool DataValid;

            if (
                string.IsNullOrWhiteSpace(Product.nome)||
                string.IsNullOrWhiteSpace(Product.nomeCientifico) ||
                string.IsNullOrWhiteSpace(Product.classificacao) ||
                string.IsNullOrWhiteSpace(Product.tempoEstimado) ||
                string.IsNullOrWhiteSpace(Product.temperatura) ||
                string.IsNullOrWhiteSpace(Product.irrigacao) ||
                string.IsNullOrWhiteSpace(Product.valorVenda.ToString()) 
                )
            {
                DataValid = false;
            }else
            {
                DataValid = true;
            }

            return DataValid;
        }

        private void ExecuteCadastro(object obj)
        {

            //Executar cadastro de produto de venda 
            if (isDetalheVisible)
            {
                //atualizar dados 
                if(_repository.ExistingData(Product))
                {
                    try
                    {
                        _repository.Edit(Product);
                        MessageBox.Show("Dados atualizados com sucesso!");
                        Product = new ProductModel();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha ao editar os dados " + ex.Message);
                    } 

                }
                else // cadastrar dados
                {
                    try
                    {
                        _repository.Add(Product);
                        MessageBox.Show("Dados cadastrado com sucesso!");
                        Product = new ProductModel();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha ao cadastrar os dados " + ex.Message);
                    }
                }
            }

        }

        
    }
}
