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
        NTQDBContext db;
        public UserDao()
        {
            db = new NTQDBContext();
        }

        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm User theo Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            try
            {
                return db.Users.SingleOrDefault(x => x.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm User theo UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            try
            {
                return db.Users.SingleOrDefault(x => x.UserName == userName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Search by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            try
            {
                return db.Users.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<User> CheckStatus(int status)
        {
            try
            {
                IQueryable<User> model = db.Users;
                if (status == 0) return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 0);
                return model.OrderByDescending(x => x.CreateAt).Where(y => y.Status == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Check ConfirmPassword
        /// </summary>
        /// <param name="confirmPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckConfirmPassword(string confirmPassword, string password)
        {
            try
            {
                if (confirmPassword == password) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            try
            {
                var name = db.Users.SingleOrDefault(x => x.UserName == userName);
                if (name == null) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.Email == email);
                if (user == null) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check User 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CheckUser(string userName, string email)
        {
            try
            {
                var name = db.Users.SingleOrDefault(x => x.UserName == userName);
                var mail = db.Users.SingleOrDefault(x => x.Email == email);
                if (name == null && mail == null)
                {
                    return 1; // thoả mãn
                }
                else if (name != null)
                {
                    return 0; //trùng userName
                }
                else
                {
                    return -1; //trùng mail
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string email, string password)
        {
            try
            {
                var result = db.Users.SingleOrDefault(x => x.Email == email);
                if (result == null)
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
                        if (result.PassWord == password)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<User> ListAllPaging(string status, string role, string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<User> model = db.Users;
                if (!string.IsNullOrEmpty(searchString))
                {

                    model = model.Where(x => x.UserName.Contains(searchString));
                }
                if (!string.IsNullOrEmpty(status))
                {
                    int Status = int.Parse(status);
                    model = model.Where(x => x.Status == Status);
                }
                if (!string.IsNullOrEmpty(role))
                {
                    int Role = int.Parse(role); 
                    model = model.Where(x => x.Role == Role);
                }
                return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                user.Status = 0;
                user.DeleteAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}
