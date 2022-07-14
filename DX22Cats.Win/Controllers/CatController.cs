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
using DX22Cats.Win.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            actAddCats = new SimpleAction(this, "AddCats", "View") { TargetObjectType = typeof(Cat) };
            actAddCats.Execute += actAddCats_Execute;
            
            actSetColor = new SimpleAction(this, "SetColor", "View") { TargetObjectType = typeof(Cat)};
            actSetColor.Execute += actSetColor_Execute;

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
            var cat = View.CurrentObject as Cat;
            var db = Helpers.MakeDbContext();
            var dCat= db.Cats.SingleOrDefault(x => x.ID == cat.ID);
            var colourList = new string[] { "Tabby","Spotty","White","Ginger","Grey"};

            var colorId = Array.IndexOf(colourList, cat.Color);
            if (colorId == 0) { cat.Color = colourList[0]; }
            colorId++;
            if (colorId >= colourList.Length) { cat.Color = colourList[0]; } else { cat.Color = colourList[colorId]; }

           


            db.Entry(dCat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            MessageBox.Show($"Set {cat.Name} colour = {cat.Color}");

            // at this point the field is updated in the list but not in the RHS

            //View.CurrentObject = cat;
            //cat.ObjectSpace.Refresh(); // no good detailview responsive but not listview
            //View.ObjectSpace.Refresh(); // causes problem
            // View.Refresh(); 

            //if (View is not ListView lv) return;
            //var savedView = (ListView)Frame.View;
            /////var caption = savedView.Caption;
            //var tr = lv.CurrentObject as IToggleRHS;
            //var ed = savedView.Editor as GridListEditor;
            //var gv = ed.GridView;
            //var rowHandle = HandyXAFWinFunctions.FindRowHandleByRowObject(gv, tr);
            //if (!Frame.SetView(null, true, null, false)) return;
           

            ////savedView.Model.MasterDetailMode = savedView.Model.MasterDetailMode == MasterDetailMode.ListViewOnly
            ////    ? MasterDetailMode.ListViewAndDetailView
            ////    : MasterDetailMode.ListViewOnly;


            //// Update the saved View according to the latest model changes and assign it back to the current Frame.
            ////savedView.LoadModel(false);
            ////savedView.Caption = caption;
            ////Frame.SetView(savedView);
            //var ed2 = savedView.Editor as GridListEditor;
            ////////HandyXAFWinFunctions.SelectRowInListView();
            //var gv2 = ed2.GridView;
            //gv2.FocusedRowHandle = rowHandle;
            //gv2.ClearSelection();
            //gv2.SelectRow(rowHandle);


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
