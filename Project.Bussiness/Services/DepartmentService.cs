using Project.DataAccess.Models;
using Project.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services
{
    class DepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) //Inject department repository
        {
            this.departmentRepository = departmentRepository;
        }


    }
}
