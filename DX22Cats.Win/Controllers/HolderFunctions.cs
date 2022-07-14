using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DX22Cats.Module.BusinessObjects;
using DX22Cats.Win.Editors;

namespace DX22Cats.Win.Controllers
{
    internal class HolderFunctions
    {
        internal static bool OpenFeature(CatFilterHolder holder,   SimpleActionExecuteEventArgs e)
        {
            var holderType = holder.GetType();
           
            var viewId = holder.Application.FindDetailViewId(holderType);



           
            var recordCount = holder.ApplyFilter();
            var maxCount = 1000;
            if (recordCount == maxCount)
            {
                MessageBox.Show(@$"There are more than {maxCount} records available, but only {maxCount} can be shown. 
                    Consider refining your filter or increasing the configuration");
            }


            var win = HandyXAFWinFunctions.GetWinIfOpen(holder.Application, viewId);
            if (win != null)
            {
                win.View.CurrentObject = holder;
                win.View.RefreshDataSource();
                e.ShowViewParameters.CreatedView = win.View;
            }
            else
            {
                var detailView = holder.Application.CreateDetailView(holder.ObjectSpace, holder);
                e.ShowViewParameters.CreatedView = detailView;
            }

            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.MdiChild;

            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            return true;
        }
    }
}