using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class UserDao
    {
        NTQDbContext db = null;
        public UserDao()
        {
            db=new NTQDbContext();
        }
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        /// <summary>
        /// Tìm kiếm User theo Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string email,string password)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == 0)
                {
                    return -1;
                }
                else
                {
                    if(result.PassWord==password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

    }
}
