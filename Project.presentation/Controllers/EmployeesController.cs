using Microsoft.AspNetCore.Mvc;
using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.Bussiness.DataTransferObjects.EmployeeDtos;
using Project.Bussiness.Services.Classes;
using Project.Bussiness.Services.Interfaces;
using System;

namespace Project.presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, IWebHostEnvironment environment, ILogger<EmployeesController> logger) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        #region Create

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid) //Server side validation
            {
                try
                {
                    int Result = _employeeService.AddEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can't be Added");
                        return View(employeeDto);
                    }
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        // 1. Development => Log error in console and return same view with error message.
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log error in file | table in database and return error view.
                        logger.LogError(ex.Message);
                    }
                }

            }
            return View(employeeDto);


        }



        #endregion
    }
}
