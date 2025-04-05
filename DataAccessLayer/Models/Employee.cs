using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int ID { get; set; } // PK By Convension
        [Required]
        [MaxLength (50)]
        public string Name { get; set; }
        public int Age { get; set; }

        public string Address { get; set; }
        public decimal Salary { get; set; }

        public bool Active { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime hiringDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }


        [ForeignKey("Department")]
        public int? DepartmentID { get; set; } = null; //FK
        public  Department Department { get; set; }
    }

}
