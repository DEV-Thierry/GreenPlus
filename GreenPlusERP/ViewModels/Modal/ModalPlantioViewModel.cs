﻿using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GreenPlusERP.ViewModels.Modal
{
    public class ModalPlantioViewModel: viewModelBase
    {
        private PlantioModel _plantio;
        private List<ProductModel> _products;
        private DataContext _context;

        public PlantioModel Plantio
        {
            get { return _plantio; } 
            set { _plantio = value; OnPropertyChanged(nameof(Plantio)); } 
        }

        public ProductModel Product
        {
            get { return _plantio.produto; }
            set
            {
                _plantio.produto = value;
                OnPropertyChanged(nameof(Product));
                OnPropertyChanged(nameof(Plantio));
            }
        }

        public List<ProductModel> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ICommand salvar {  get; set; }



        public ModalPlantioViewModel()
        {
            _context = new DataContext();
            _plantio = new PlantioModel
            {
                produto = new ProductModel() 
            };
            _products = _context.Products.ToList();

            salvar = new viewModelCommand(executeSave, canExecuteSave);
        }

        private void executeSave(object obj)
        {
            if (obj is Window window)
            {
                DateTimeOffset dataAtual = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(-3));
                _plantio.previsaoColheita = dataAtual.DateTime.AddMonths(_plantio.produto.TempoEstimado);
                _plantio.dataPlantio = dataAtual.DateTime;
                _context.Plantio.Add(_plantio);
                _context.SaveChanges();
                
                window.Close();
                
            }
        }

        private bool canExecuteSave(object obj)
        {
            bool canExecute;
            if(Plantio.lote == 0 ||
                Plantio.quantidade == 0)
            {
                canExecute = false;
            }
            else
            {
                canExecute = true;
            }

            return canExecute;
        }
    }
}