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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX22Cats.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CatFilterHolderController : ViewController
    {
        SimpleAction actCatsScreen;
        //SimpleAction actCurrentJobs;

        public CatFilterHolderController() : base()
        {
            TargetViewNesting = Nesting.Root;
            actCatsScreen = new SimpleAction(this, "Cats", "View")  ;
            actCatsScreen.Execute += actCatsScreen_Execute;


            //actCurrentJobs = new SimpleAction(this, "CurrentJobsFromJobs", "Filters")
            //{ Caption = "Current Orders", ImageName = "Current-Jobs", ToolTip = "Open current jobs" };
            //actCurrentJobs.Execute += actCurrentJobs_Execute;
        }
        private void actCatsScreen_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var filter = new CatFilter();
            var holder = new CatFilterHolder(filter) { };
            HolderFunctions.OpenFeature(holder, Application, e);
        }
    }
}
