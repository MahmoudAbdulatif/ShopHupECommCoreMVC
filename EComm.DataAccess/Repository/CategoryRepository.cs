using EComm.DataAccess.Data;
using EComm.DataAccess.Repository.IRepository;
using EComm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EComm.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) :base(db) 
        {
            _db = db;
        }
        

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
