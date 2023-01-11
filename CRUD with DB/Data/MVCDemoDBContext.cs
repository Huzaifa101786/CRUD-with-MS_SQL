using CRUD_with_DB.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUD_with_DB.Data
{
    public class MVCDemoDBContext : DbContext
    {
        public MVCDemoDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
