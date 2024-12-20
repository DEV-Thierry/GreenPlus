﻿using GreenPlusERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public class productRepository : repositoryBase, IProductRepository
    {
        private DataContext _context = new DataContext();
        public void Add(ProductModel productModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [Produto] values(@nome, @nomeCientifico, @classificacao, @tempoEstimado, @temperatura, @irrigacao, @valorVenda)";
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = productModel.NomePlanta;
                command.Parameters.Add("@nomeCientifico", SqlDbType.VarChar).Value = productModel.NomeCientifico;
                command.Parameters.Add("@classificacao", SqlDbType.VarChar).Value = productModel.Classificacao;
                command.Parameters.Add("@tempoEstimado", SqlDbType.Int).Value = productModel.TempoEstimado;
                command.Parameters.Add("@temperatura", SqlDbType.Decimal).Value = productModel.Temperatura;
                command.Parameters.Add("@irrigacao", SqlDbType.Int).Value = productModel.Irrigacao;
                command.Parameters.Add("@valorVenda", SqlDbType.Float).Value = productModel.ValorVenda;

                command.ExecuteNonQuery();
            }
        }

        public void AddInterno(Interno interno)
        {
            _context.Internos.Add(interno);
            _context.SaveChanges();
        }

        public void DeleteInterno(int id)
        {
            var interno = _context.Internos.FirstOrDefault(x => x.id == id);
            if(interno != null)
            {
                _context.Internos.Remove(interno);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("erro");
            }
        }

        public void Edit(ProductModel productModel)
        {
            using(var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open(); command.Connection = connection;
                command.CommandText = "update [Produto] set NomePlanta = @nome, Classificacao = @classificacao, TempoEstimado = @tempo, Temperatura = @temp, Irrigacao = @irrigacao, ValorVenda = @valor where NomeCientifico = @nomeCien";
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value= productModel.NomePlanta.Trim();
                command.Parameters.Add("@classificacao", SqlDbType.VarChar).Value= productModel.Classificacao.Trim();
                command.Parameters.Add("@tempo", SqlDbType.Int).Value= productModel.TempoEstimado;
                command.Parameters.Add("@temp", SqlDbType.Decimal).Value= productModel.Temperatura;
                command.Parameters.Add("@irrigacao", SqlDbType.Int).Value= productModel.Irrigacao;
                command.Parameters.Add("@valor", SqlDbType.Float).Value= productModel.ValorVenda;
                command.Parameters.Add("@nomeCien", SqlDbType.VarChar).Value= productModel.NomeCientifico.Trim();

                command.ExecuteNonQuery();
            }
        }

        public bool ExistingData(ProductModel productModel)
        {
            bool existingData;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from [Produto] where NomeCientifico = @nomeCientifico";
                command.Parameters.Add("@nomeCientifico", SqlDbType.VarChar).Value = productModel.NomeCientifico.Trim();

                existingData = command.ExecuteScalar() == null ? false : true;
            }

            return existingData;
        }

        public bool ExistingInternoData(Interno interno)
        {
            var produto = _context.Internos.FirstOrDefault(x => x.id == interno.id);

            if(produto == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ObservableCollection<ProductModel> GetAll()
        {
           throw new NotImplementedException();
        }

        public ProductModel GetByName(string name)
        {
            var product = _context.Products.FirstOrDefault(x => x.NomePlanta == name);

            if(product != null)
            {
                return product;
            }
            else
            {
                return new ProductModel();
            }
            
        }

        public Interno GetInternoByName(string nome)
        {
            var interno = _context.Internos.FirstOrDefault(x => x.nome == nome);
            if(interno != null)
            {
                return interno;
            }
            else
            {
                return new Interno();
            }
        }

        public void Remove(string nomeCientifico)
        {
            var Produto = _context.Products.FirstOrDefault(x => x.NomeCientifico == nomeCientifico);

            if (Produto != null)
            {
                _context.Products.Remove(Produto);
                _context.SaveChanges();
            }
            else {
                return;
            }

        }

        public void UpdateInterno(Interno interno)
        {
            var produto = _context.Internos.FirstOrDefault(x => x.id == interno.id);

            if (produto != null)
            {
                produto.nome = interno.nome;
                produto.descricao = interno.descricao;
                produto.estado = interno.estado;
                _context.Internos.Update(produto);
                _context.SaveChanges();
            }
        }
    }
}
