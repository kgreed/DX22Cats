using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DX22Cats.Module.Functions;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace DX22Cats.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    public class CatFilterHolder : IFilterHolder , IObjectSpaceLink
    {
        [NotMapped]
        [Browsable(false)]
        public XafApplication Application { get; set; }

        public CatFilterHolder(CatFilter filter) {
            Filter = filter;
        }

        public CatFilterHolder(CatFilter filter, XafApplication application) : this(filter)
        {
            this.Application = application;
            ObjectSpace = Application.CreateObjectSpace(typeof(Cat));  // any valid type would have done

        }

        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        [NotMapped]
        [Browsable(false)] public IObjectSpace ObjectSpace { get; set; }

        public CatFilter Filter { get; set; }
        public virtual List<Cat> Cats { get; set; }
        

        public int ApplyFilter()
        {
            var line = 10;
            try
            {
                Cats = new List<Cat>();
                if (ObjectSpace == null) return 0;
                ObjectSpace = Application.CreateObjectSpace(typeof(Cat));  // any valid type would have done
                 

                var search = Helpers.SafeString(Filter.Search);
                var sql = "Select Name,Color,IsVerified,Id from Cats ";
                if (search.Length > 0) sql = sql+ $"where name like  '%{search}%'";  

                var db = Helpers.MakeDbContext();

                var results = db.Cats.FromSqlRaw(sql).ToList();

                foreach (Cat r in results)
                {
                    r.ObjectSpace = ObjectSpace;
                    var r2 = ObjectSpace.GetObject(r);
                    if (r.Color != r2.Color)
                    {
                        throw new Exception($"What the? {r.Color} <> {r2.Color}");
                    }
                    var n = r2.Foods.Count(); // necessary to avoid LazyLoading Errors https://discourse.softwarebydesign.com.au/t/lazy-loading-errors-on-orders/4385
                    r2.ResetRHSResult();
                    foreach (var line2 in r2.Foods)
                    {
                        line2.ResetJobRHSResult();
                    }

                    Cats.Add(r2);

                }
               
               
                return Cats.Count;
               
            }
            catch (Exception ex)
            {
                throw new Exception($"ApplyFilter line{line}, {ex.InnerException}, {ex}");
                
            }
        }

        public int ListCount()
        {
            return Cats.Count;
        }
    }
}
