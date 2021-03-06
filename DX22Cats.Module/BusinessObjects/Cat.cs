using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DX22Cats.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<EntityObject1> EntityObject1s { get; set; }" syntax.


    // Register this entity in the DbContext using the "public DbSet<EntityObject1> EntityObject1s { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("StatusVerified", FontColor = "#009A00", TargetItems = "*", Criteria = "Verified" )] // green 
    public class Cat : MyBaseObject, IToggleRHS
    {
        public Cat()
        {
            Foods = new List<Food>();
        }

        internal void ResetRHSResult()
        {
            //throw new NotImplementedException();
        }
        [Key]
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        // You can use the regular Code First syntax:
        public string Name { get; set; }

        //Alternatively, specify more UI options: 
        private string _Color;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                   OnPropertyChanged();
                }
            }
        }
        
        public virtual List<Food> Foods { get; set; }
        private bool _verified;
        [Column("IsVerified")]
        [ToolTip("Indicates the cat has been verified")]
        [ImmediatePostData]
        public bool Verified{ get => _verified; set { _verified = value; OnPropertyChanged(); } }

        public int Key => ID;
        // Collection property:
        //public virtual IList<AssociatedEntityObject> AssociatedEntities { get; set; }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


    }
}
