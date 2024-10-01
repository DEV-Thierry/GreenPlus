using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        bool ExistingData(ProductModel productModel);
        ProductModel GetByName(ProductModel product);
        void Edit(ProductModel productModel);
        void Remove(string NomeCientifico);
        IEnumerable<ProductModel> GetAll();
    }
}
