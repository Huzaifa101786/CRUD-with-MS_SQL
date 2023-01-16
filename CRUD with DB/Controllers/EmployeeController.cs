using CRUD_with_DB.Data;
using CRUD_with_DB.Models;
using CRUD_with_DB.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_with_DB.Controllers
{
    public class EmployeeController : Controller
    {
        private MVCDemoDBContext mvcDemoDbContext;

        public EmployeeController(MVCDemoDBContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ShowEmployee()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel addmodel)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addmodel.Name,
                Email = addmodel.Email,
                Salary = addmodel.Salary,
                Department = addmodel.Department,
                DateOfBirth = addmodel.DateOfBirth,
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("AddEmployee");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("ShowEmployee");
            }
            return RedirectToAction("ShowEmployee");
        }
    }
}
