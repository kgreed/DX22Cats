using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Win.Editors;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Module.Functions;
using DX22Cats.Win.Editors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DX22Cats.Module.Controllers
{


    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CatController : ViewController
    {
        SimpleAction actAddCats;
        SimpleAction actSetColor;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CatController()
        {
            actAddCats = new SimpleAction(this, "AddCats", "View") { };
            actAddCats.Execute += actAddCats_Execute;

            actSetColor = new SimpleAction(this, "SetColor", "View") { };
            actSetColor.Execute += actSetColor_Execute;
            TargetObjectType = typeof(Cat);
            TargetViewType = ViewType.ListView;
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void actAddCats_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var db = Helpers.MakeDbContext();
            for (int i = 0; i < 100; i++)
            {
                var cat = new Cat
                {
                    Name = $"Cat {i}"
                };

                for (int j = 0; j < 10; j++)
                {
                    cat.Foods.Add(new Food { Description = $"Food {j}" });
                }
                db.Cats.Add(cat);
            }
            db.SaveChanges();
            View.Refresh();
        }
        private void actSetColor_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();  // start with save objects

            // works if colour property has INotifyPropertyChanges
            var cat = View.CurrentObject as Cat;
            var colourList = new string[] { "Tabby", "Spotty", "White", "Ginger", "Grey", "Perrywinkle", "Blue" };
            var colorId = Array.IndexOf(colourList, cat.Color);

            colorId++;
            // if (colorId >= colourList.Length) { cat.Color = colourList[0]; } else { cat.Color = colourList[colorId]; }




            var db = Helpers.MakeDbContext();
            var dCat = db.Cats.Include(x=>x.Foods).SingleOrDefault(x => x.ID == cat.ID);
            if (colorId >= colourList.Length) { dCat.Color = colourList[0]; } else { dCat.Color = colourList[colorId]; }

            db.Entry(dCat).State =  EntityState.Modified;
            db.SaveChanges();

            //var changes = db.ChangeTracker.HasChanges() ? "Changes" : "None";
           // MessageBox.Show(changes);
            var lv = ((ListView)View);
            lv.CollectionSource.ResetCollection(true);
            lv.RefreshDataSource();
            lv.EditView.RefreshDataSource();
            lv.EditView.Refresh();
            var os = View.ObjectSpace;
            cat.Foods = dCat.Foods;
            os.ReloadObject(cat);
             
            //os.CommitChanges();
            //os.Refresh();
            //cat.ObjectSpace = os;
            
            //cat.ObjectSpace.Refresh();
            //var os = cat.ObjectSpace;
            //cat = ObjectSpace.GetObject(cat);
            //os.Rollback();
            //var conditionalRefreshController = Frame.GetController<AppearanceController>();
            //conditionalRefreshController.Refresh();
            //cat.Foods = dCat.Foods;
            //cat = View.CurrentObject as Cat;

            // changes = db.ChangeTracker.HasChanges() ? "Changes" : "None";
            //MessageBox.Show(changes);
            MessageBox.Show($"Set {dCat.Name} new colour = {dCat.Color}  , old cat ={cat.Color} , Foods {cat.Foods.Count} , DFoods {dCat.Foods.Count}");

            

        }


        private void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            if (GlobalSingleton.Instance.RefreshCats)
            {
                GlobalSingleton.Instance.RefreshCats = false;
                var h = View.CurrentObject as Cat;
                if (h == null)
                    return;


                var lv = View as ListView;
                var dv = View as DetailView ?? lv?.EditFrame?.View as DetailView;
                if (dv == null)
                    return;

                dv.RefreshDataSource();
                dv.Refresh();

                var viewItem = dv.Items.SingleOrDefault(x => x.Id == "Foods");
                viewItem.Refresh();
            }
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
