using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;

namespace GreenPlusERP.Repositorios
{

    public class FornecedorRepository : repositoryBase, IFornecedorRepository
    {

        private DataContext _context = new DataContext();
        public void Add(FornecedorModel fornecedor)
        {
            using(var connection = GetConnection())
            using(var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "insert into [Fornecedor] values (@cnpj, @razaoSocial, @situacao, @email, @contato, @nomeResponsalve, @logradouro, @numero, @bairro, @complemento, @cep, @uf, @municipio)";
                command.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = Regex.Replace(fornecedor.cnpj, "[^0-9]+", "");
                command.Parameters.Add("@razaoSocial", SqlDbType.VarChar).Value = fornecedor.razaoSocial;
                command.Parameters.Add("@situacao", SqlDbType.VarChar).Value = fornecedor.situacao;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = fornecedor.email;
                command.Parameters.Add("@contato", SqlDbType.VarChar).Value = fornecedor.contato;
                command.Parameters.Add("@nomeResponsalve", SqlDbType.VarChar).Value = fornecedor.nomeResponsavel;
                command.Parameters.Add("@logradouro", SqlDbType.VarChar).Value = fornecedor.logradouro;
                command.Parameters.Add("@numero", SqlDbType.VarChar).Value = fornecedor.numero;
                command.Parameters.Add("@bairro", SqlDbType.VarChar).Value = fornecedor.bairro;
                command.Parameters.Add("@complemento", SqlDbType.VarChar).Value = fornecedor.complemento;
                command.Parameters.Add("@cep", SqlDbType.VarChar).Value = fornecedor.cep;
                command.Parameters.Add("@uf", SqlDbType.VarChar).Value = fornecedor.uf;
                command.Parameters.Add("@municipio", SqlDbType.VarChar).Value = fornecedor.municipio;

                command.ExecuteNonQuery();
            }
        }

        public FornecedorModel cnpjByApi(string cnpj)
        {
            FornecedorModel fornec = new FornecedorModel();
            var empresa = Empresa.ObterCnpj(cnpj);
            if (empresa != null)
            {
                fornec.cnpj = empresa.cnpj;
                fornec.razaoSocial = empresa.nome;
                fornec.situacao = empresa.situacao;
                fornec.email = empresa.email;
                fornec.contato = empresa.telefone;
                fornec.logradouro = empresa.logradouro;
                fornec.numero = empresa.numero;
                fornec.bairro = empresa.bairro;
                fornec.complemento = empresa.complemento;
                fornec.cep = empresa.cep;
                fornec.uf = empresa.uf;
                fornec.municipio = empresa.municipio;
            }

            return fornec;
        }

        public void Edit(FornecedorModel fornecedor)
        {
            throw new NotImplementedException();
        }

        //public FornecedorModel GetByCnpj(string cnpj)
        //{
        //    FornecedorModel Fornecedor = null;

        //    using (var connection = GetConnection())
        //    using (var command = new SqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "SELECT * FROM [Fornecedor] where cnpj = @cnpj";
        //        command.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = Regex.Replace(cnpj, "[^0-9]+", "");

        //        using (var reader = command.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                Fornecedor = new FornecedorModel()
        //                {
        //                    cnpj = reader[1].ToString(),
        //                    razaoSocial = reader[2].ToString(),
        //                    situação = reader[3].ToString(),
        //                    email = reader[4].ToString(),
        //                    contato = reader[5].ToString(),
        //                    nomeResponsável = reader[6].ToString(),
        //                    logradouro = reader[7].ToString(),
        //                    numero = reader[8].ToString(),
        //                    bairro = reader[9].ToString(),
        //                    complemneto = reader[10].ToString(),
        //                    cep = reader[11].ToString(),
        //                    uf = reader[12].ToString(),
        //                    municipio = reader[13].ToString()
        //                };
        //            }
        //        }
        //    }

        //    return Fornecedor;
        //}
        public FornecedorModel GetByCnpj(string cnpj)
        {
                // Remove caracteres não numéricos do CNPJ
                string cnpjLimpo = Regex.Replace(cnpj, "[^0-9]+", "");

                // Busca o fornecedor pelo CNPJ usando LINQ
                var fornecedor = _context.Fornecedores
                                          .FirstOrDefault(f => f.cnpj == cnpjLimpo);

            if ( fornecedor == null )
            {
                MessageBox.Show("Nenhum fornecedor encontrado");
            }

                return fornecedor;
            
        }

        public FornecedorModel GetByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string cnpj)
        {
            int count = 0;
            

            return count;
        }

        public void Remove(string cnpj, string nome)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [Fornecedor] where cnpj=@cnpj AND nomeResponsavel = @nomeResponsavel ";
                command.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = Regex.Replace(cnpj, "[^0-9]+", "");
                command.Parameters.Add("@nomeResponsavel", SqlDbType.VarChar).Value = nome;

                command.ExecuteNonQuery();
            }
        }

        public bool VerificaCNPJ(string cnpj)
        {
            bool existe;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Fornecedor] where cnpj = @cnpj";
                command.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = Regex.Replace(cnpj, "[^0-9]+", "");

                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        existe = true;
                    }else
                    {
                        existe= false;
                    }
                }
            }

            return existe;
        }
    }
}
