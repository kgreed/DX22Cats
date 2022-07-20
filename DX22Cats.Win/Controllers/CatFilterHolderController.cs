using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win.Editors;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Module.Functions;
using DX22Cats.Win.Editors;
using System;
using System.Linq;

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
            actCatsScreen = new SimpleAction(this, "Cats", "Filters");
            actCatsScreen.Execute += actCatsScreen_Execute;


            //actCurrentJobs = new SimpleAction(this, "CurrentJobsFromJobs", "Filters")
            //{ Caption = "Current Orders", ImageName = "Current-Jobs", ToolTip = "Open current jobs" };
            //actCurrentJobs.Execute += actCurrentJobs_Execute;
        }
        private void actCatsScreen_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var filter = new CatFilter();
            var holder = new CatFilterHolder(filter, Application) { };
            HolderFunctions.OpenFeature(holder, e);
        }
        private void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            //if (GlobalSingleton.Instance.RefreshCatFilterHolder)
            //{
            //    GlobalSingleton.Instance.RefreshCatFilterHolder = false;
                //var h = View.CurrentObject as CatFilterHolder;
                //if (h == null)
                //    return;


                //var lv = View as ListView;
                //var dv = View as DetailView ?? lv?.EditFrame?.View as DetailView;
                //if (dv == null)
                //    return;

                //dv.RefreshDataSource();
                //dv.Refresh();

                //var viewItem = dv.Items.SingleOrDefault(x => x.Id == "Foods");
                //viewItem.Refresh();
           // }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            View.CurrentObjectChanged += View_CurrentObjectChanged;
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
