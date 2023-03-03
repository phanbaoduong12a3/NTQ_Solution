using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public interface IUserService
    {
        int Insert(User entity);
        User GetByEmail(string email);
        IEnumerable<User> ListAllPaging(int page, int pageSize);
    }
}
