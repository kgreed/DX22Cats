using DevExpress.Persistent.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DX22Cats.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<EntityObject1> EntityObject1s { get; set; }" syntax.
    [DefaultClassOptions]
    public class Food : MyBaseObject, IToggleRHS
    {


        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int CatId { get; set; }
        [ForeignKey("CatId")]
        public virtual Cat Cat { get; set; }
        public int Key => Id;
        internal void ResetJobRHSResult()
        {
            //throw new NotImplementedException();
        }
    }
}
