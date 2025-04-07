using Project.DataAccess.Data.Contexts;
using Project.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository, ApplicationDbContext dbContext)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public int saveChanges() => _dbContext.SaveChanges();

    }
}
