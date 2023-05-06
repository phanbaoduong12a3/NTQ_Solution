using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class CategoryDao
    {
        NTQDBContext db;
        public CategoryDao()
        {
            db = new NTQDBContext();
        }
        public IEnumerable<Category> ListCategory(string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<Category> model = db.Categories;
                if(!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.CategoryName.Contains(searchString));
                }
                return model.OrderBy(x=>x.ID).ToPagedList(page, pageSize);
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
        public bool CheckCategoryName(string categoryName)
        {
            var model = db.Categories.Where(x=>x.CategoryName == categoryName);
            if (model != null) return false;
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
            var model = db.Categories.Find(id);
            model.Status = false;
            db.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            try
            {
                var model = db.Categories.Find(category.ID);
                model.Status = category.Status;
                model.CategoryName = category.CategoryName;
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
