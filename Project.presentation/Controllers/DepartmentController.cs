using Microsoft.AspNetCore.Mvc;
using Project.Bussiness.DataTransferObjects;
using Project.Bussiness.Services;

namespace Project.presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _enviroment) : Controller
    {

        // BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #region Create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid) //Server side validation
            {
                try
                {
                    int Result = _departmentService.CreateDepartment(departmentDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't be created");
                        return View(departmentDto);
                    }
                }
                catch (Exception ex)
                {
                    if (_enviroment.IsDevelopment())
                    {
                        // 1. Development => Log error in console and return same view with error message.
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(departmentDto);
                    }
                    else
                    {
                        // 2. Deployment => Log error in file | table in database and return error view.
                        _logger.LogError(ex.Message);
                        return View(departmentDto);
                    }
                }

            }
            return View(departmentDto);

        }


        #endregion

        #region Department details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }

        #endregion

    }

}
