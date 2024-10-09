using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using GreenPlusERP.Views.Modals;
using Microsoft.EntityFrameworkCore;

namespace GreenPlusERP.ViewModels
{
    public class PlantioViewModel: viewModelBase
    {
        private DataContext _context;
        private ObservableCollection<PlantioModel> _plants;


        public ObservableCollection<PlantioModel> plants
        {
            get { return _plants; }
            set { _plants = value; OnPropertyChanged(nameof(plants)); }
        }


        //public List<PlantioModel> plantacao
        //{
        //    get { return _plantacao; }
        //    set { _plantacao = value; OnPropertyChanged(nameof(plantacao)); }
        //}

        public ICommand incluirPlantio {  get; set; }

        public PlantioViewModel()
        {
            _context = new DataContext();
            plants = new ObservableCollection<PlantioModel>(
            _context.Plantio.Include(p => p.produto).OrderBy(x => x.dataPlantio).ToList()
        );
            incluirPlantio = new viewModelCommand(executeInclusao);
        }

        private void executeInclusao(object obj)
        {
            modalPlantio modal = new modalPlantio();
            modal.ShowDialog();
        }
    }
}
