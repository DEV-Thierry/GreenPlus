using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public abstract class repositoryBase
    {
        private readonly string _connectionString;
        public repositoryBase()
        {
            _connectionString = "server=localhost\\SQLEXPRESS;database=GreenPlus;trusted_connection=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
