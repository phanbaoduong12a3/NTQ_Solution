using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
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
        /// Tìm kiếm User theo UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User GetById(int id)
        {
            return db.Users.Find(id);
        }
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CheckUser(string userName,string email)
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
                return -1; //trùng mail
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
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderBy(x=>x.Create_at).ToPagedList(page,pageSize);
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                if (user != null)
                {
                    user.UserName = entity.UserName;
                    user.PassWord = entity.PassWord;
                    user.Email = entity.Email;
                    user.Update_at = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                return false;
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
