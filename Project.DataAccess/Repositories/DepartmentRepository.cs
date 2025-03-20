using Project.DataAccess.Data.Contexts;

namespace Project.DataAccess.Repositories
{
    class DepartmentRepository(ApplicationDbContext dbContext)
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //CRUD operations
        //Get All
        //Get By Id
        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }

        //Update
        //Delete
        //Insert
    }
}
