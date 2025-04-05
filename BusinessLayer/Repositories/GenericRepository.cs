using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppDbContext _dbContext;
        public GenericRepository(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T Item)
        {
            _dbContext.Add(Item);

        }

        public void Delete(T Item)
        {
            _dbContext.Remove(Item);

        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) _dbContext.Employees.Include(E => E.Department).ToList();
            return _dbContext.Set<T>().ToList();
        }
         
        public T GetByID(int id)

        => _dbContext.Set<T>().Find(id);
          

        public void Update(T Item)
        {
            _dbContext.Update(Item);

        }


    }
}
