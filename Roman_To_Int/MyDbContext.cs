using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Roman_To_Int
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        { 
        }

        public DbSet <BallInfo> ballInfos { get; set; }
    }
}
