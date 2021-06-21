using EFCoreLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreLib
{
    public class EfCore: DbContext
    {
        public EfCore(DbContextOptions options) : base(options)
        {
        }

        DbSet<Employee> Employees { get; set; }
    }
}
