using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win.Editors;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Win.Editors;
using System;
using System.Linq;

namespace DX22Cats.Win.Controllers
{
    public partial class ToggleRHSController : ViewController
    {
        SimpleAction actToggleView;
        public ToggleRHSController()
        {
            actToggleView = new SimpleAction(this, "Toggle RHS", "View")
            {
                Shortcut = "Control+Shift+A",
                ImageName = "FolderPanel"
            };
            actToggleView.Execute += actToggleView_Execute;

            TargetObjectType = typeof(IToggleRHS);
            TargetViewType = ViewType.ListView;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }




        private void actToggleView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is not ListView lv) return;
            var savedView = (ListView)Frame.View;
            var caption = savedView.Caption;
            var tr = lv.CurrentObject as IToggleRHS;
            var ed = savedView.Editor as GridListEditor;
            var gv = ed.GridView;
            var rowHandle = HandyXAFWinFunctions.FindRowHandleByRowObject(gv, tr);
            if (!Frame.SetView(null, true, null, false)) return;


            savedView.Model.MasterDetailMode = savedView.Model.MasterDetailMode == MasterDetailMode.ListViewOnly
                ? MasterDetailMode.ListViewAndDetailView
                : MasterDetailMode.ListViewOnly;


            // Update the saved View according to the latest model changes and assign it back to the current Frame.
            savedView.LoadModel(false);
            savedView.Caption = caption;
            Frame.SetView(savedView);
            var ed2 = savedView.Editor as GridListEditor;
            //HandyXAFWinFunctions.SelectRowInListView();
            var gv2 = ed2.GridView;
            gv2.FocusedRowHandle = rowHandle;
            gv2.ClearSelection();
            gv2.SelectRow(rowHandle);

        }


    }
}
