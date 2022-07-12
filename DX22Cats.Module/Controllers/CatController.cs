using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Module.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX22Cats.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CatController : ViewController
    {
        SimpleAction actSetColor;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CatController()
        {
            actSetColor = new SimpleAction(this, "SetColor", "View");
            actSetColor.Execute += actSetColor_Execute;
            
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void actSetColor_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var cat = View.CurrentObject as Cat;
            var db = Helpers.MakeDbContext();
            var dCat= db.Cats.SingleOrDefault(x => x.ID == cat.ID);

            var s = cat.Color ?? "";
            switch (s)
            {
                case "": s = "tabby"; break;
                case "tabby": s = "spotty"; break;
                case "spotty": s = "grey"; break;
                default: s = "black"; break;

            }
            dCat.Color = s;
            db.Entry(dCat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            View.ObjectSpace.Refresh();
            View.Refresh();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
