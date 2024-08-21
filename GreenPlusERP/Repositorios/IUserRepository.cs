using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(userModel userModel);
        void Edit(userModel userModel);
        void Remove(int id);
        userModel GetByName(string Name);
        userModel GetByUsername(string username);
        IEnumerable<userModel> GetAll();
    }
}
