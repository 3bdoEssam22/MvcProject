using Microsoft.AspNetCore.Mvc;
using Project.Bussiness.Services;

namespace Project.presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {

        // BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = departmentService.GetAllDepartments();

            return View(departments);
        }
    }
}
