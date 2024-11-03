using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        bool ExistingData(ProductModel productModel);
        bool ExistingInternoData(Interno interno);
        ProductModel GetByName( string name);
        void Edit(ProductModel productModel);
        void Remove(string NomeCientifico);
        ObservableCollection<ProductModel> GetAll();
        void AddInterno(Interno interno);
        Interno GetInternoByName(string nome);
        void UpdateInterno(Interno interno);
        void DeleteInterno(int id);
    }
}
