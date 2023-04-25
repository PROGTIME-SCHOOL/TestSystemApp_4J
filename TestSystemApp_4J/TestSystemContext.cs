using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemApp_4J.Models;

namespace TestSystemApp_4J
{
    class TestSystemContext : DbContext
    {
        public DbSet<Question> Question { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Test> Test { get; set; }
        public DbSet<TestQuestion> TestQuestion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.UseSqlite($"Data Source={path}TestSystem.db");
        }
    }
}
