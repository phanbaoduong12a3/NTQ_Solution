using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class UserDao
    {
        NTQDBContext db = null;
        public UserDao()
        {
            db=new NTQDBContext();
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

        /// <summary>
        /// Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// Check Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<User> CheckStatus(int status)
        {
            IQueryable<User> model = db.Users;
            if (status == 0) return model.OrderByDescending(x => x.CreateAt).Where(x=>x.Status == 0);
            return model.OrderByDescending(x=>x.CreateAt).Where(y=>y.Status == 1);

        }

        /// <summary>
        /// Check ConfirmPassword
        /// </summary>
        /// <param name="confirmPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckConfirmPassword(string confirmPassword, string password)
        {
            if (confirmPassword == password) return true;
            return false;
        }
        
        /// <summary>
        /// Check UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            var name = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (name == null) return true;
            return false;
        }

        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email);
            if (user == null) return true;
            return false;
        }

        /// <summary>
        /// Check User 
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
        public IEnumerable<User> ListAllPaging(string active,string inActive,string admin,string user, string searchString,int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(active) || !string.IsNullOrEmpty(inActive) || !string.IsNullOrEmpty(admin) || !string.IsNullOrEmpty(user) )
            {
                model = model.Where(x => x.UserName.Contains(searchString));
                if(active != null)
                {
                    model = model.Where(x => x.Status == 1);
                }
                if(inActive != null)
                {
                    model = model.Where(x => x.Status == 0);
                }
                if(admin != null)
                {
                    model = model.Where(x => x.Role == 1);
                }
                if(user != null)
                {
                    model = model.Where(x => x.Role == 0);
                }
                if (model == null)
                {
                    return null;
                }
                return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                if (user != null)
                {
                    user.UserName = entity.UserName;
                    user.PassWord = entity.PassWord;
                    user.UpdateAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                user.Status = 0;
                user.DeleteAt= DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
