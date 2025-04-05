using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class MVCAppDbContext : DbContext
    {

        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options)
        {

        }
       
       public DbSet<Department> Departments { get; set; }

       public DbSet<Employee> Employees { get; set; }
    }
}
