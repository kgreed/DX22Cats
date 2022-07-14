using System;
using System.Linq;

namespace DX22Cats.Module.BusinessObjects
{
    public class CatFilter
    {
        public CatFilter(){
            Search = "";
            }
        public string Search { get; set; }
    }
}
