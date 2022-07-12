using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DX22Cats.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DX22Cats.Module.Functions
{
    public static class Helpers
    {


        public static DX22CatsEFCoreDbContext MakeDbContext()
        {
           var  connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            
            var optionsBuilder = new DbContextOptionsBuilder<DX22CatsEFCoreDbContext>()
                .UseSqlServer(connectionString);

            return new DX22CatsEFCoreDbContext(optionsBuilder.Options);
        }
    }
}
