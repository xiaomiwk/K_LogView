using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Win
{
    public partial class F主窗口 : DevExpress.XtraEditors.XtraForm
    {
        public F主窗口()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.out标签列表.CustomHeaderButtonClick += out标签列表_CustomHeaderButtonClick;
            this.out标签列表.CloseButtonClick += out标签列表_CloseButtonClick;
            创建新窗口(true);
            创建新窗口();

        }

        void out标签列表_CloseButtonClick(object sender, EventArgs e)
        {
            var __arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
            if (__arg != null && __arg.Page is DevExpress.XtraTab.XtraTabPage)
            {
                this.out标签列表.TabPages.Remove((DevExpress.XtraTab.XtraTabPage)__arg.Page);
            }
        }

        void out标签列表_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            创建新窗口();
        }

        private void 创建新窗口(bool __加载默认文件 = false)
        {
            var __page = new DevExpress.XtraTab.XtraTabPage();
            __page.Text = "未指定";
            var __内容 = new F文本分析(__加载默认文件) { Dock = DockStyle.Fill };
            __内容.加载事件 += q => { __page.Text = string.Format("{0}({1})", q[0].FullName, q.Count); };
            __page.Controls.Add(__内容);
            this.out标签列表.TabPages.Add(__page);
        }
    }
}
