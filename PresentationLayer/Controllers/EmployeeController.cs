using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PresentationLayer.Helper;
using PresentationLayer.ViewModels;
using System.Reflection.Metadata;


namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public IMapper _mapper { get; }

        public EmployeeController(IUnitOfWork unitOfWork /*Ask CLR for object from Class implement interface IUnitOfWork*/, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        //BasURl/Department/Index

        public IActionResult Index(string Search)
        {
            IEnumerable <Employee> employees;
            if (string.IsNullOrEmpty(Search))
                 employees = _unitOfWork.EmployeeRepository.GetAll();
              else   
                 employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(Search);    
                 var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                 return View(MappedEmployees);    
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Departments = DepartmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {

               employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images"); 
               var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
               _unitOfWork.EmployeeRepository.Add(MappedEmployee); 
               _unitOfWork.Complete();
               return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        //BaseURl/Department/Details/id
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //status Code 400
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployees = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployees);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    // 1- log exception
                    // 2- Form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }


        [HttpGet] // By default
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.ID)
                return BadRequest();
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }

        
    }
}

