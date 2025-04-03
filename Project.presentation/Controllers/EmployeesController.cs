using Microsoft.AspNetCore.Mvc;
using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.Bussiness.DataTransferObjects.EmployeeDtos;
using Project.Bussiness.Services.Classes;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Models.EmployeesModel;
using Project.DataAccess.Models.Shared.Enums;
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

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);

        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var employeeDto = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDto);

        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, UpdatedEmployeeDto employeeDto)
        {
            if (!id.HasValue || id != employeeDto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeDto);
            try
            {
                var reult = _employeeService.UpdateEmployee(employeeDto);
                if (reult > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't be Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    // 1. Development => Log error in console and return same view with error message.
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
                }
                else
                {
                    // 2. Deployment => Log error in file | table in database and return error view.
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Result = _employeeService.DeleteEmployee(id);
                if (Result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't be deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    // 1. Development => Log error in console and return same view with error message.
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 2. Deployment => Log error in file | table in database and return error view.
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }

        }
        #endregion
    }
}
