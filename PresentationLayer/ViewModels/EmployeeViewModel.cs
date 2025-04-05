using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels
{
    public class EmployeeViewModel
    {
        public int ID { get; set; } 

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 characters")]
        [MinLength(3, ErrorMessage = "Min Length is 3 Characters")]
        public string  Name { get; set; }

        [Range(22, 60, ErrorMessage = "the Employee Age Must be in Range 22 - 60")]
        public int  Age { get; set; }

        public string  Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool Active { get; set; }

        [EmailAddress]
        public string  Email { get; set; }

        [Phone]
        public string  PhoneNumber { get; set; }
        public DateTime hiringDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int  DepartmentID { get; set; } //FK
        public Department  Department { get; set; }
    }
}
