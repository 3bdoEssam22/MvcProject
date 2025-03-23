using Project.Bussiness.DataTransferObjects.EmployeeDtos;
using Project.DataAccess.Models.EmployeesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Factories
{
    static class EmployeeFactory
    {

        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
            };
        }
        public static EmployeeDeatilsDto ToEmployeeDeatilsDto(this Employee employee)
        {
            return new EmployeeDeatilsDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                Adress = employee.Adress,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber
            };
        }
        public static Employee ToEntity(this CreatedEmployeeDto employeeDto)
        {
            return new Employee
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Adress = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = employeeDto.CreatedBy,
                LastModifiedBy = employeeDto.LastModifiedBy

            };
        }
        public static Employee ToEntity(this UpdatedEmployeeDto employeeDto) => new Employee
        {
            Name = employeeDto.Name,
            Age = employeeDto.Age,
            Adress = employeeDto.Address,
            IsActive = employeeDto.IsActive,
            Salary = employeeDto.Salary,
            Email = employeeDto.Email,
            PhoneNumber = employeeDto.PhoneNumber,
            HiringDate = employeeDto.HiringDate,
            Gender = employeeDto.Gender,
            EmployeeType = employeeDto.EmployeeType,
            CreatedBy = employeeDto.CreatedBy,
            LastModifiedBy = employeeDto.LastModifiedBy
        };

    }
}
