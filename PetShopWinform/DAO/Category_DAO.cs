using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShopWinform.Model;

namespace PetShopWinform.DAO
{
    class Category_DAO
    {
        private PetshopWinformEntities DBPetShop;

        public Category_DAO() { DBPetShop = new PetshopWinformEntities(); }
        
        public dynamic getDanhSachCategory()
        {
            var danhSach = DBPetShop.Categories.Select(c => new
            {
                c.Id,
                c.Name
            }).ToList();
            return danhSach;
        }

        public void addCategory(Category category)
        {
            DBPetShop.Categories.Add(category);
            DBPetShop.SaveChanges();
        }

        public void editCategory(Category category)
        {
            var categoryFind = findCategory(category);

            categoryFind.Name = category.Name;
            DBPetShop.SaveChanges();
        }

        public void deleteCategory(Category category)
        {
            var categoryFind = findCategory(category);

            DBPetShop.Categories.Remove(categoryFind);
            DBPetShop.SaveChanges();
        }

        public Category findCategory(Category category)
        {
            var categoryFind = DBPetShop.Categories.Find(category.Id);
            return categoryFind;
        }
    }
}
