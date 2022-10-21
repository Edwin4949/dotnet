using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace Repository_Layer
{
    public class AppDBcontext : DbContext
    {
        public AppDBcontext(DbContextOptions dboptions) : base(dboptions)
        {
        }

        public DbSet<EmployeeRegistration> Employee { get; set; }
        public DbSet<AdminVerify> Admin { get; set; }
    }
}