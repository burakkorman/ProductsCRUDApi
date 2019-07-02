using ProductsCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Services
{
    interface IAccountService
    {
        string Login(UserDTO user);
        void Create(UserDTO user);
        void Update(int id, UserDTO user);
        void Delete(int userId);
    }
}
