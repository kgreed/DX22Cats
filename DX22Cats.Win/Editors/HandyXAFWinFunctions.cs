using DevExpress.ExpressApp;
using DevExpress.XtraGrid.Views.Grid;
using DX22Cats.Module.BusinessObjects;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win;

namespace DX22Cats.Win.Editors
{
    internal class HandyXAFWinFunctions
    {
        internal static void RefreshAndWarnMismatchIfNeeded(CatFilterHolder holder, CompositeView view)
        {
            var recordCount = holder.ApplyFilter();


            HandyXAFFunctions.WarnRecordsCountIfNeeded(recordCount);

            GridView gv = null;
            var detailView = view as DetailView;
            int lvCount = 0;
            ListView lv;
            var counter = 0;
            foreach (ListPropertyEditor lpe in detailView.GetItems<ListPropertyEditor>())
            {
                counter++;
                lv = lpe.ListView;
                lv.CollectionSource.ResetCollection(true);
                lv.RefreshDataSource();
                lv.Refresh();
                var gridEditor = lv.Editor as GridListEditor;
                gv = gridEditor.GridView;
                lvCount = gv.RowCount;
            }

          
            if (counter != 1)
            {
                throw new Exception("Expected 1 and only 1 lv");
            }
            if (lvCount != recordCount)
            {

                MessageBox.Show($"Record count {recordCount}, lvCount {lvCount} . Work around by closing and reopening.");
                //view.RefreshDataSource();
            }
        }

        internal static WinWindow GetWinIfOpen(XafApplication application, string listViewId)
        {
            if (!(application.ShowViewStrategy is WinShowViewStrategyBase strategy)) return null;
            foreach (var win in strategy.Windows.ToArray())
            {
                if (win.View == null) continue;
                if (!win.View.Id.Equals(listViewId)) continue;
                return win;
                //win.View.Close();


            }
            return null;
        }
    }
}