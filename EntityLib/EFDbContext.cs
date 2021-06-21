using EntityLib.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityLib
{
    public class EFDbContext: DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }
        DbSet<Employee> Employees { get; set; }
    }
}
