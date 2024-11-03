using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenPlusERP.ViewModels
{
    public class ProductViewModel : viewModelBase
    {
        //fields
        private ProductModel _product;
        private string _pesquisa;
        private string _pesquisaInterno;
        private Interno _interno;
        IProductRepository _repository;
        private bool isDetalheVisible;

        public ObservableCollection<string> ProductNames { get; set; }
        public ObservableCollection<string> ProductInternosNames { get; set; }

        public ProductModel Product 
        {
            get { return _product; } 
            set { _product = value; OnPropertyChanged(nameof(Product));}
        }

        public string PesquisaInterno
        {
            get { return _pesquisaInterno;}
            set { _pesquisaInterno = value; OnPropertyChanged(nameof(PesquisaInterno)); LoadSuggestionsInterno(); }
        }
        
        public string Pesquisa
        {
            get { return _pesquisa;}
            set { _pesquisa = value; OnPropertyChanged(nameof(Pesquisa)); LoadSuggestions(); }
        }


        public Interno interno
        {
            get { return _interno; }
            set { _interno = value; OnPropertyChanged(nameof(interno)); }
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
        public ICommand CadastroInternoCommand {  get; set; }
        public ICommand ConsultarInternoCommand { get; set; }
        public ICommand DeletarInternoCommand { get; set; }

        //constructor
        public ProductViewModel()
        {
            _repository = new productRepository();
            Product = new ProductModel();
            interno = new Interno();
            CadastroCommand = new viewModelCommand(ExecuteCadastro, CanExecuteCadastro);
            ConsultarCommand = new viewModelCommand(ExecuteConsulta, CanExecuteConsulta);
            DeletarCommand = new viewModelCommand(ExecuteDelete, CanExecuteDelete);
            CadastroInternoCommand = new viewModelCommand(ExecuteInternoCadastro, CanExecuteInternoCadastro);
            ConsultarInternoCommand = new viewModelCommand(ExecuteInternoConsulta, CanExecuteInternoConsulta);
            DeletarInternoCommand = new viewModelCommand(ExecuteInternoDelete, CanExecuteInternoDelete);
            Product.ValorVenda = 0;
            isDetalheVisible = true;
            ProductNames = new ObservableCollection<string>();
            ProductInternosNames = new ObservableCollection<string>();
        }

        //methods   
        private bool CanExecuteInternoDelete(object obj)
        {
            if (string.IsNullOrWhiteSpace(interno.nome) ||
                string.IsNullOrWhiteSpace(interno.descricao) ||
                string.IsNullOrWhiteSpace(interno.estado))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteInternoDelete(object obj)
        {
            string confirma = "Tem certeza que deseja deletar o produto: \"" + PesquisaInterno.ToString() + "\"";
            string caption = "Deletando dados";
            var result = MessageBox.Show(confirma, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.DeleteInterno(interno.id);
                    MessageBox.Show("Produto deletado com sucesso!");
                    interno = new Interno();
                    PesquisaInterno = "";
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+ "Produto não encontrado para ser deletado");
                }
                
            }

        }

        private bool CanExecuteInternoConsulta(object obj)
        {
            if(string.IsNullOrWhiteSpace(PesquisaInterno))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteInternoConsulta(object obj)
        {
            try
            {
                interno = _repository.GetInternoByName(PesquisaInterno);
                if(interno.nome == null)
                {
                    MessageBox.Show("Produto não encontrado");
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExecuteInternoCadastro(object obj)
        {
            bool required;

            if(string.IsNullOrWhiteSpace(PesquisaInterno) ||
                string.IsNullOrWhiteSpace(interno.descricao) ||
                string.IsNullOrWhiteSpace(interno.estado))
            {
                required = false;
            }else
            {
                required = true;
            }

            return required;
        }

        private void ExecuteInternoCadastro(object obj)
        {
            interno.nome = PesquisaInterno;
            if(_repository.ExistingInternoData(interno))
            {
                _repository.UpdateInterno(interno);
                MessageBox.Show("Dados atualizados com sucesso!");
                interno = new Interno();
                PesquisaInterno = "";
            }
            else
            {
                _repository.AddInterno(interno);
                MessageBox.Show("Produto cadastrado com sucesso!");
                interno = new Interno();
                PesquisaInterno = "";
            }
        }

        private bool CanExecuteDelete(object obj)
        {
            if(string.IsNullOrWhiteSpace(Product.NomeCientifico))
            {
                return false;
            }
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
            string confirma = "Tem certeza que deseja deletar o produto: \"" + Product.NomePlanta.ToString() + "\"";
            string caption = "Deletando dados";
            var result = MessageBox.Show(confirma, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _repository.Remove(Product.NomeCientifico);
                MessageBox.Show($"prduto: {Product.NomePlanta} deletado com sucesso!");
                Product = new ProductModel();
                Pesquisa = "";
            }
        }

        private bool CanExecuteConsulta(object obj)
        {
            if(string.IsNullOrWhiteSpace(Pesquisa))
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
                Product = _repository.GetByName(Pesquisa);
                if(Product.NomePlanta == null)
                {
                    MessageBox.Show("Produto não encontrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao realizar a consulta " + ex.Message);
            }
        }


        private bool CanExecuteCadastro(object obj)
        {
            bool DataValid;

            if (
                string.IsNullOrWhiteSpace(Pesquisa)||
                string.IsNullOrWhiteSpace(Product.NomeCientifico) ||
                string.IsNullOrWhiteSpace(Product.Classificacao) ||
                Product.TempoEstimado == null ||
                Product.Temperatura == null ||
                Product.Irrigacao == null ||
                Product.ValorVenda == null
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
            Product.NomePlanta = Pesquisa;
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
                        Pesquisa = "";
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
                        Pesquisa = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha ao cadastrar os dados " + ex.Message);
                    }
                }
            }

        }


        public void LoadSuggestions()
        {
            using (var context = new DataContext())
            {
                var suggestions = context.Products
                    .Where(p => p.NomePlanta.Contains(Pesquisa))
                    .Select(p => p.NomePlanta)
                    .ToList();

                ProductNames.Clear();
                foreach (var name in suggestions)
                {
                    ProductNames.Add(name);
                }
            }
        }
        
        public void LoadSuggestionsInterno()
        {
            using (var context = new DataContext())
            {
                var suggestions = context.Internos
                    .Where(p => p.nome.Contains(PesquisaInterno))
                    .Select(p => p.nome)
                    .ToList();

                ProductInternosNames.Clear();
                foreach (var name in suggestions)
                {
                    ProductInternosNames.Add(name);
                }
            }
        }


    }
}
