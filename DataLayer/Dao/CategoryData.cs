using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class CategoryData
    {
        NTQDBContext db;
        public CategoryData()
        {
            db = new NTQDBContext();
        }
        /// <summary>
        /// Danh sách danh mục
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Category> ListCategory(string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<Category> categories = db.Categories;
                if(!string.IsNullOrEmpty(searchString))
                {
                    categories = categories.Where(x => x.CategoryName.Contains(searchString));
                }
                return categories.OrderBy(x=>x.ID).ToPagedList(page, pageSize);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public Category GetByID(int id)
        {
            return db.Categories.Find(id);
        }
        /// <summary>
        /// Kiểm tra danh mục đã tồn tại chưa theo tên danh mục
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public bool CheckCategoryName(string categoryName)
        {
            var categories = db.Categories.Where(x=>x.CategoryName == categoryName);
            if (categories != null) return false;
            return true;
        }
        public void InsertCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void Delete(int id)
        {
            var category = db.Categories.Find(id);
            category.Status = false;
            db.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            try
            {
                var categories = db.Categories.Find(category.ID);
                categories.Status = category.Status;
                categories.CategoryName = category.CategoryName;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
