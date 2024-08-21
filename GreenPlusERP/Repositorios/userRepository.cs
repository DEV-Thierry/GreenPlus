using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public class userRepository : repositoryBase, IUserRepository
    {
        public void Add(userModel userModel)
        {
            using (var connection  = GetConnection()) 
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [User] values(@tipo, @nome, @email, @cargo, @grupo, @username, @senha)";
                command.Parameters.Add("@tipo", SqlDbType.Bit).Value = true;
                command.Parameters.Add("@nome", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userModel.Email;
                command.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = userModel.Cargo;
                command.Parameters.Add("@grupo", SqlDbType.NVarChar).Value = userModel.Grupo;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.UserName;
                command.Parameters.Add("@senha", SqlDbType.NVarChar).Value = userModel.Password;

                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool isValidUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username=@username and senha=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                isValidUser = command.ExecuteScalar() == null ? false : true;
            }

            return isValidUser;
        }

        public void Edit(userModel userModel)
        {
            using(var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update [User] set nome = @nome, cargo = @cargo, grupos = @grupo, senha = @senha where username = @username";
                command.Parameters.Add("@nome", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = userModel.Cargo;
                command.Parameters.Add("@grupo", SqlDbType.NVarChar).Value = userModel.Grupo;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.UserName;
                command.Parameters.Add("@senha", SqlDbType.NVarChar).Value = userModel.Password;

                command.ExecuteNonQuery();

            }
        }

        public IEnumerable<userModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public userModel GetByName(string Name)
        {
            userModel userModel = null;

            using( var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = command.CommandText = "select *from [User] where nome=@name";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = Name;

                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        userModel = new userModel()
                        {
                            Id = reader[0].ToString(),
                            Tipo = reader[1].ToString(),
                            Name = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Cargo = reader[4].ToString(),
                            Grupo = reader[5].ToString(),
                            UserName = reader[6].ToString(),
                            Password = reader[7].ToString(),
                        };
                    }
                }
            }

            return userModel;
        }

        public userModel GetByUsername(string username)
        {
            userModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new userModel()
                        {
                            Id = reader[0].ToString(),
                            Tipo = reader[1].ToString(),
                            Name = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Cargo = reader[4].ToString(),
                            Grupo = reader[5].ToString(),
                            UserName = reader[6].ToString(),
                            Password = reader[7].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }


    }
}
