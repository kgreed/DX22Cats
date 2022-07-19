using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Module.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DX22Cats.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FoodController : ViewController
    {
        SimpleAction actPasteFood;
        SimpleAction actCopyFood;

        Food mFood;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public FoodController()
        {
            actCopyFood = new SimpleAction(this, "CopyFood", "View");
            actCopyFood.Execute += actCopyFood_Execute;
            actPasteFood = new SimpleAction(this, "PasteFood", "View");
            actPasteFood.Execute += actPasteFood_Execute;
            TargetObjectType = typeof(Food);
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Nested;
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void ReportCount(ListView lv)
        {
            var gridEditor = lv.Editor as GridListEditor;
            var gv = gridEditor.GridView;
            gv.RefreshData();
            var lvCount = gv.RowCount;
            Debug.Print(lvCount.ToString());

        }
        private void actPasteFood_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            if (mFood == null)
            {
                MessageBox.Show("You need to copy before pasting");
                return;
            }
            var db = Helpers.MakeDbContext();
            var cat = db.Cats.Where(x => x.ID == mFood.Cat.ID).SingleOrDefault();
            var food = new Food
            {
                Cat =  cat,
                Description = mFood.Description
            };
            db.Foods.Add(food);
            db.SaveChanges();
            
            var holderDetailView = View.ObjectSpace.Owner as DetailView;
            var holder = holderDetailView.CurrentObject as CatFilterHolder;
           
            //holder.ObjectSpace.Refresh();
            holder.Cats.Clear();    
            holder.ApplyFilter();
            var firstCatFoods = holder.Cats.FirstOrDefault().Foods.Count;
           // holderDetailView.ObjectSpace.Refresh();
            // holder.ObjectSpace.Refresh();

             //var win = Application.MainWindow;
            //win.SetView(holderDetailView);
            //win.View.RefreshDataSource();
           // var fr = (NestedFrame)Frame;

            //((ListView)View).ObjectSpace.Refresh();   WORKS IF I DO THIS BUT I lose the conditional appearance

            var refreshController = Frame.GetController<RefreshController>();
            var refreshAction = refreshController.RefreshAction;
            Debug.Print(refreshAction.Active.ResultValue.ToString()); // false
            Debug.Print(refreshAction.Enabled.ResultValue.ToString()); // true
           // refreshController.RefreshAction.DoExecute();






             var fr = (NestedFrame)Frame;
            //var viewItem = fr.ViewItem;
            //var dv = viewItem.View as DetailView;

            //dv.RefreshDataSource();
            //dv.Refresh();

            //var vi = dv.Items.SingleOrDefault(x => x.Id == "Foods");
            //var lpe = vi as ListPropertyEditor;
            //lpe.RefreshDataSource();
            //var lv = lpe.ListView;
            //lv.CollectionSource.ResetCollection(true);  // brings in data


            //ReportCount(lv);
            ////lv.CollectionSource.ResetCollection(true);
            ////lv.CollectionSource.Reload();
            //lv.RefreshDataSource(); 
            //ReportCount(lv);
            //lv.Refresh();


            //Debug.Print($"{lv.Items.Count()}");
            //ReportCount(lv);

            //var lv = View as ListView;
            //lv.ObjectSpace.Refresh();
            //lv.CollectionSource.ResetCollection(true);  // brings in data

            //lv.RefreshDataSource();



            //lv.CollectionSource.ResetCollection(true);
            //lv.RefreshDataSource();
            //lv.EditView?.RefreshDataSource();
            //lv.EditView?.Refresh();
            //var refreshed = View.ObjectSpace.Refresh(); // causes conditional appearance module to stop working
            //if (!refreshed) MessageBox.Show("Not refreshed");
            //The nested list view's frame is represented by the NestedFrame class that has the ViewItem property,
            //which returns the ListPropertyEditor from the parent DetailView.
            //It is possible to access the parent DetailView via the ListPropertyEditor's View property.
            //https://supportcenter.devexpress.com/ticket/details/e3977/how-to-access-a-nested-listview-from-the-parent-detailview-s-controller-and-vice-versa
            //var fr = (NestedFrame)Frame;


            ////parentDetailView.ObjectSpace.Refresh();
            ////parentDetailView.RefreshDataSource();

            //var viewItem=    fr.ViewItem;
            //var dv = viewItem.View as DetailView;
            ////var obj = dv.CurrentObject;
            ////dv.ObjectSpace.ReloadObject(obj);
            //dv.ObjectSpace.Refresh();
            //dv.RefreshDataSource();
            //dv.Refresh();
            //dv.ObjectSpace.CommitChanges();
            //dv.RefreshDataSource();
            //dv.Refresh();

            //  var parent = lv.ObjectSpace.GetObject(((NestedFrame)Frame).ViewItem.CurrentObject);

            //var mainWin = Application.MainWindow.View; null
            //Helpers.RefreshRHSDetailView(lvFood);
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        private void actCopyFood_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            mFood = View.CurrentObject as Food;
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
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
