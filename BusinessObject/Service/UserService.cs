using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class UserService : IUserService
    {
        private readonly NTQDbContext _context;
        private readonly IUserService _userService;
        public UserService(NTQDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return _context.Users.OrderBy(x => x.Create_at).ToPagedList(page, pageSize);
        }
    }
}
