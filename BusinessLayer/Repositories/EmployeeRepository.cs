 using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository 
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepository(MVCAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string Address)
        => _dbContext.Employees?.Where(E => E.Address == Address);

        public IQueryable<Employee> GetEmployeesByName(string Search)
        =>
            _dbContext.Employees.Where(E => E.Name.ToLower().Contains(Search.ToLower()));
             
        
    }
}
