using Project.Bussiness.DataTransferObjects.EmployeeDtos;
using Project.Bussiness.Factories;
using Project.Bussiness.Services.Interfaces;
using Project.DataAccess.Models.EmployeesModel;
using Project.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services.Classes
{
    class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        public EmployeeDeatilsDto? GetEmployeeById(int id)
        {
            var employee = employeeRepository.GetById(id);
            return employee == null ? null : employee.ToEmployeeDeatilsDto();
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = employeeRepository.GetAll();
            return employees.Select(e => e.ToEmployeeDto());
        }
        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = employeeDto.ToEntity();
            return employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return employeeRepository.Update(employeeDto.ToEntity());
        }

        public bool DeleteEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee == null)
                return false;
            else
            {
                int Reault = employeeRepository.Delete(employee);
                return Reault > 0 ? true : false;
            }
        }

    }
}
