using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmpResumeBuilder.Models
{
    public class EmpDataContext : DbContext
    {
        //public EmpDataContext()
        //   : base("name=MySqlConnection")
        //{
        //}
        public DbSet<Employee> employees { get; set; }
    }
}