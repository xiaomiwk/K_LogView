using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Win
{
    public partial class F查询提示 : DevExpress.XtraEditors.XtraForm
    {
        private static readonly string _类型名 = typeof(F查询提示).Name;

        public F查询提示()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode)
            {
                return;
            }

            //设置属性;
            //订阅事件;
            this.do返回.Click += do返回_Click;

        }

        void do返回_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
