using DX22Cats.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DX22Cats.Win.Editors
{
    public partial class CatFilterControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CatFilterControl()
        {
            InitializeComponent();
        }
        public event Action ApplyFilter = delegate { };
       
        public CatFilter  Filter { get; set; }
        private void CatFilterControl_Load(object sender, EventArgs e)
        {
            textBox1.Text = Filter.Search;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Filter.Search = textBox1.Text;
            ApplyFilter();
        }
    }
  
}
