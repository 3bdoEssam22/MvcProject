using Microsoft.AspNetCore.Mvc;
using Project.Bussiness.Services.Interfaces;

namespace Project.presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService) :Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}
