using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public int Register(string userName,string email)
        {
            var name = db.Users.SingleOrDefault(x=>x.UserName == userName);
            var mail = db.Users.SingleOrDefault(x => x.Email == email);
            if(name == null && mail == null)
            {
                return 1; // thoả mãn
            }
            else if(name != null)
            {
                return 0; //trùng userName
            }
            else
            {
                return -1; //trùng Email
            }

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
                return 0; // không thấy email
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
                        return -2; //sai passWord
                    }
                }
            }
        }

    }
}
