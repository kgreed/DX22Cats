using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DX22Cats.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DX22Cats.Module.Functions
{
        public static class Helpers
    {
        public static void RefreshRHSDetailView(View view)
        {
            if (view is not ListView lv) return;
            var dv = lv.EditFrame?.View as DetailView;
            dv?.Refresh();
        }
        public static string SafeString(string s)
        {
            Regex r = new Regex("^[a-zA-Z0-9 ]*$");
            if (!r.IsMatch(s))
            {
                throw new Exception(s + "is not alphanumeric or space. Please only enter numbers letters or spaces in your search.");
            }
            return s;

        }
        public static DX22CatsEFCoreDbContext MakeDbContext()
        {
           var  connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            
            var optionsBuilder = new DbContextOptionsBuilder<DX22CatsEFCoreDbContext>()
                .UseSqlServer(connectionString);

            return new DX22CatsEFCoreDbContext(optionsBuilder.Options);
        }
    }
}
