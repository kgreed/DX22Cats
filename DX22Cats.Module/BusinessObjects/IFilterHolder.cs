using DevExpress.ExpressApp;
using System;
using System.Linq;

namespace DX22Cats.Module.BusinessObjects
{
    public interface IFilterHolder
    {
        IObjectSpace ObjectSpace { get; set; }
        public int ListCount();

        public int ApplyFilter();

    }
}
