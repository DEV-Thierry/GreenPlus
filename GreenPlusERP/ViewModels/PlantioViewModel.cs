using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;

namespace GreenPlusERP.ViewModels
{
    public class PlantioViewModel: viewModelBase
    {
        private PlantioModel _plantacao;
        DataContext _context;
        private List<ProductModel> _plants;

        public List<ProductModel> plants
        {
            get { return _plants; }
            set { _plants = _context.Products.ToList(); OnPropertyChanged(nameof(plants)); }
        }


        public PlantioModel plantacao
        {
            get { return _plantacao; }
            set { _plantacao = value; OnPropertyChanged(nameof(plantacao)); }
        }

        public PlantioViewModel()
        {
            _context = new DataContext();  // Supondo que tenha uma implementação concreta
            plants = _context.Products.ToList(); // Carrega os dados no ObservableCollection
            plantacao = new PlantioModel();
        }
    }
}
