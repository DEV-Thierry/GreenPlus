using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public class productRepository : repositoryBase, IProductRepository
    {
        public void Add(ProductModel productModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [Produto] values(@nome, @nomeCientifico, @classificacao, @tempoEstimado, @temperatura, @irrigacao, @valorVenda)";
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = productModel.nome;
                command.Parameters.Add("@nomeCientifico", SqlDbType.VarChar).Value = productModel.nomeCientifico;
                command.Parameters.Add("@classificacao", SqlDbType.VarChar).Value = productModel.classificacao;
                command.Parameters.Add("@tempoEstimado", SqlDbType.Int).Value = int.Parse(productModel.tempoEstimado);
                command.Parameters.Add("@temperatura", SqlDbType.Decimal).Value = decimal.Parse(productModel.temperatura);
                command.Parameters.Add("@irrigacao", SqlDbType.Int).Value = int.Parse(productModel.irrigacao);
                command.Parameters.Add("@valorVenda", SqlDbType.Float).Value = productModel.valorVenda;

                command.ExecuteNonQuery();
            }
        }

        public void Edit(ProductModel productModel)
        {
            using(var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open(); command.Connection = connection;
                command.CommandText = "update [Produto] set NomePlanta = @nome, Classificacao = @classificacao, TempoEstimado = @tempo, Temperatura = @temp, Irrigacao = @irrigacao, ValorVenda = @valor where NomeCientifico = @nomeCien";
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value= productModel.nome;
                command.Parameters.Add("@classificacao", SqlDbType.VarChar).Value= productModel.classificacao;
                command.Parameters.Add("@tempo", SqlDbType.Int).Value= int.Parse(productModel.tempoEstimado);
                command.Parameters.Add("@temp", SqlDbType.Decimal).Value= decimal.Parse(productModel.temperatura);
                command.Parameters.Add("@irrigacao", SqlDbType.Int).Value= int.Parse(productModel.irrigacao);
                command.Parameters.Add("@valor", SqlDbType.Float).Value= productModel.valorVenda;
                command.Parameters.Add("@nomeCien", SqlDbType.VarChar).Value= productModel.nomeCientifico;

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
                command.Parameters.Add("@nomeCientifico", SqlDbType.VarChar).Value = productModel.nomeCientifico;

                existingData = command.ExecuteScalar() == null ? false : true;
            }

            return existingData;
        }

        public IEnumerable<ProductModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductModel GetByName(ProductModel product)
        {
            ProductModel Product = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Produto where NomePlanta = @nome";
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = product.nome;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new ProductModel()
                        {
                            id = reader[0].ToString(),
                            nome = reader[1].ToString(),
                            nomeCientifico = reader[2].ToString(),
                            classificacao = reader[3].ToString(),
                            tempoEstimado = reader[4].ToString(),
                            temperatura = reader[5].ToString(),
                            irrigacao = reader[6].ToString(),
                            valorVenda = float.Parse(reader[7].ToString()),
                        };

                    }

                    return product;
                }
            }
        }

            public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
