using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win.Model;

namespace Win
{
    public partial class F详细信息 : DevExpress.XtraEditors.XtraForm
    {
        private M详细信息 _当前详细信息;

        public F详细信息(M详细信息 __M详细信息)
        {
            InitializeComponent();
            _当前详细信息 = __M详细信息;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.out标题.Text = _当前详细信息.标题;
            this.out等级.Text = _当前详细信息.等级.ToString();
            this.out内容.Text = _当前详细信息.内容;
            this.out时间.Text = _当前详细信息.时间.ToString("MM-dd HH:mm:ss.FFF");
            this.out线程.Text = _当前详细信息.线程;

            var __其他 = new StringBuilder();
            if (_当前详细信息.跟踪标记 != 0)
            {
                __其他.AppendFormat("跟踪标记: \t{0};\t", _当前详细信息.跟踪标记);
                __其他.AppendFormat("跟踪周期: \t{0};\t", _当前详细信息.跟踪周期);
                __其他.AppendFormat("耗时: \t{0};\r\n", _当前详细信息.耗时);
            }
            if (!string.IsNullOrEmpty(_当前详细信息.文件))
            {
                __其他.AppendFormat("文件: \t{0};\r\n", _当前详细信息.文件);
            }
            if (!string.IsNullOrEmpty(_当前详细信息.类型))
            {
                __其他.AppendFormat("类型: \t{0};\r\n", _当前详细信息.类型);
            }
            if (!string.IsNullOrEmpty(_当前详细信息.方法))
            {
                __其他.AppendFormat("方法: \t{0};\r\n", _当前详细信息.方法);
            }
            if (!string.IsNullOrEmpty(_当前详细信息.行号))
            {
                __其他.AppendFormat("行号: \t{0};\r\n", _当前详细信息.行号);
            }

            this.out其他.Text = __其他.ToString();
            this.do关闭.Focus();

            this.do复制内容.Click += do复制内容_Click;
            this.do关闭.Click += do关闭_Click;
        }

        void do关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void do复制内容_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_当前详细信息.内容);
        }
    }
}
