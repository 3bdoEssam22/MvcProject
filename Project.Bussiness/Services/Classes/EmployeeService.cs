using AutoMapper;
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
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var employees = _employeeRepository.GetAll(withTracking).Where(e => !e.IsDeleted).ToList();
            //var employeesDto = _employeeRepository.GetAll(E => new EmployeeDto
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Salary = E.Salary,
            //    Age = E.Age,
            //}).Where(E => E.Age > 25);
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

            return employeesDto;

            //return employees.Select(e => e.ToEmployeeDto());
        }
        public EmployeeDeatilsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return employee == null ? null : _mapper.Map<Employee, EmployeeDeatilsDto>(employee);
        }
        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
                return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }

    }
}
