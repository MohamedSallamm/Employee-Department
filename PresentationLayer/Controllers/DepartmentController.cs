using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
          
            _unitOfWork = unitOfWork;
        }

        //BasURl/Department/Index

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            //1- View Data >> KeyValuePair (Dictionary Object)
            //To transfer message from Controller (Action) To it's View,
            //it's exist from DotNet 3.5
            // ViewData["Message"] = "Hello from View Data";
            //2- viewBag >> It's Dynamic Property 
            //To transfer message from Controller (Action) To it's View,
            //it's exist from DotNet 4.0
            // ViewBag.Message = "Hello From View Data";

            //3- Temp Data >> Dictionary Object 
            // Transfer Data from Action To action

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
               int Result = _unitOfWork.Complete();
                //3- Temp Data >> Dictionary Object 
                // Transfer Data from Action To action
                if (Result > 0)
                {
                    TempData["Message"] = "Department Created Succefully";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        //BasURl/Department/Details/id
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); //status Code 400
            var department = _unitOfWork.DepartmentRepository.GetByID(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName, department);
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetByID(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (id != department.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
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
            return View(department);
        }
        [HttpGet] // By default
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.ID)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }
        }
    }
}


