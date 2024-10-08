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
        ProductModel GetByName( string name);
        void Edit(ProductModel productModel);
        void Remove(string NomeCientifico);
        ObservableCollection<ProductModel> GetAll();
    }
}
