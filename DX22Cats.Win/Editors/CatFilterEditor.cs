using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DX22Cats.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DX22Cats.Win.Editors
{
    [PropertyEditor(typeof(CatFilter), true)]
    public class CatFilterEditor : WinPropertyEditor //, IComplexViewItem
    {
        public CatFilterEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }
        public override bool IsCaptionVisible => false;
        protected override object CreateControlCore()
        {
            var filter = (CatFilter)PropertyValue;
            var control = new CatFilterControl { Filter=  filter };
            control.ApplyFilter += Control_ApplyFilter;
            return control;
        }

        protected override void ReadValueCore()
        {
            base.ReadValueCore();
            SetControlValue();
        }

        private void SetControlValue()
        {
            if (!(PropertyValue is CatFilter filter)) return;
            ((CatFilterControl)Control).Filter = filter;
        }

        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            ReadValue();
        }

        protected override object GetControlValueCore()
        {
            return ((CatFilterControl)Control).Filter;
        }
        private void Control_ApplyFilter()
        {
            var holder = View.CurrentObject as CatFilterHolder;

            HandyXAFWinFunctions.RefreshAndWarnMismatchIfNeeded(holder, View);

        }
    }
}
