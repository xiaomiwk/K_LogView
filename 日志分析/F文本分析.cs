using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout.Events;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using Win.IBLL;
using Win.Model;

namespace Win
{
    public partial class F文本分析 : XtraUserControl
    {
        private readonly I文件分析 _I文本分析 = IBLLFactory.获取文本分析();

        private M概要信息 _概要信息;

        private List<M详细信息> _详细信息列表;

        private List<M详细信息> _分组信息列表;

        private M详细信息 _启动信息;

        private string _日志路径;

        private bool _加载默认文件;

        public F文本分析(bool __加载默认文件 = false)
        {
            _加载默认文件 = __加载默认文件;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
            {
                return;
            }

            this.in启动次数_时段.SelectedIndex = 1;
            if (_加载默认文件)
            {
                this.加载日志文件();
            }

            this.out当前启动次数.Text = string.Empty;

            this.do刷新文件路径.Click += do刷新文件路径_Click;
            this.do指定时间_确定.Click += do指定时间_确定_Click;
            this.do启动次数_第一次.Click += do启动次数_第一次_Click;
            this.do启动次数_最后一次.Click += do启动次数_最后一次_Click;
            this.do启动次数_上一次.Click += do启动次数_上一次_Click;
            this.do启动次数_下一次.Click += do启动次数_下一次_Click;
            this.do启动次数_确定.Click += do启动次数_确定_Click;
            this.out详细信息表格.RowCellStyle += out详细信息表格_RowCellStyle;
            this.out详细信息表格.RowStyle += out详细信息表格_RowStyle;
            this.out详细信息表格.DoubleClick += out详细信息表格_DoubleClick;
            this.out概要信息表格.Click += out概要信息表格_Click;
            this.out概要信息表格.CustomFieldValueStyle += out概要信息表格_CustomFieldValueStyle;
            this.out警告信息表格.Click += out警告信息表格_Click;

            this.do启动次数_确定.PerformClick();
            //this.out详细信息表格.TopRowIndex = this.out详细信息表格.RowCount - 1;
            //this.out详细信息表格.MoveLast();

            //启用拖动文件到程序，然后解析的功能
            this.out文件路径.AllowDrop = true;
            this.out文件路径.DragEnter += (sender1, e1) => e1.Effect = DragDropEffects.Link;
            this.out文件路径.DragDrop += 文件路径_DragDrop;

            this.do过滤提示.Click += (sender1, e1) => new F查询提示().ShowDialog();

            this.do加入右键.Visible = !H注册表.验证是否为所有文件添加操作系统右键菜单("浏览日志", Assembly.GetExecutingAssembly().Location);
            this.do加入右键.Click += do加入右键_Click;

            this.do高亮.Click += do高亮_Click;
            this.do过滤.Click += do过滤_Click;
            this.do清除.Click += do清除_Click;
            this.in查询条件.Properties.ButtonClick += Properties_ButtonClick;
            this.in查询条件.Properties.KeyDown += Properties_KeyDown;
            this.in查询条件.Properties.Items.AddRange(new object[] { "时间:\"??:??\"", "标题:\"???\" +内容:\"???\"" });


        }

        void Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.do过滤.PerformClick();
            }
        }

        void Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    this.in查询条件.ResetText();
                    break;
                case 1:
                this.do过滤.PerformClick();
                    break;
            }
        }

        void do清除_Click(object sender, EventArgs e)
        {
            this.in查询条件.Text = string.Empty;
            this.out详细信息表格.ApplyFindFilter(string.Empty);
            this.out详细信息表格.FormatConditions.Clear();
        }

        void do过滤_Click(object sender, EventArgs e)
        {
            this.out详细信息表格.ApplyFindFilter(this.in查询条件.Text.Trim());
        }

        void do高亮_Click(object sender, EventArgs e)
        {
            var condition = new StyleFormatCondition { Condition = FormatConditionEnum.Expression };
            condition.Appearance.BackColor = Color.FromArgb(255, 210, 0);
            //condition.Appearance.ForeColor = Color.White;
            condition.Appearance.Options.UseBackColor = true;
            condition.Appearance.Options.UseForeColor = true;
            condition.ApplyToRow = false;
            this.out详细信息表格.FormatConditions.Clear();
            this.out详细信息表格.FormatConditions.Add(condition);
            condition.Expression = string.Format("([线程] LIKE '%{0}%') OR ([标题] LIKE '%{0}%') OR ([内容] LIKE '%{0}%')", this.in查询条件.Text.Trim());
            //using (ExpressionEditorForm form = new ConditionExpressionEditorForm(condition, null))
            //{
            //    form.StartPosition = FormStartPosition.CenterParent;
            //    if (form.ShowDialog(this) == DialogResult.OK)
            //    {
            //        condition.Expression = form.Expression;
            //    }
            //}
        }

        void do加入右键_Click(object sender, EventArgs e)
        {
            H注册表.为所有文件添加操作系统右键菜单("浏览日志", Assembly.GetExecutingAssembly().Location);
            this.do加入右键.Visible = !H注册表.验证是否为所有文件添加操作系统右键菜单("浏览日志", Assembly.GetExecutingAssembly().Location);
        }

        public void 文件路径_DragDrop(object sender, DragEventArgs e)
        {
            this.out文件路径.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            do刷新文件路径.PerformClick();
        }

        private void out概要信息表格_Click(object sender, EventArgs e)
        {
            var __绑定数据 = this.out概要信息表格.GetRow(this.out概要信息表格.FocusedRowHandle) as M详细信息;
            if (__绑定数据 == null)
            {
                return;
            }
            this.out详细信息表格.FocusedRowHandle = this.out详细信息表格.GetRowHandle(__绑定数据.Id - _启动信息.Id);
            this.out详细信息表格.TopRowIndex = this.out详细信息表格.FocusedRowHandle;
        }

        private void out详细信息表格_DoubleClick(object sender, EventArgs e)
        {
            if (this.out详细信息表格.FocusedRowHandle < 0)
            {
                return;
            }
            var __绑定数据 = this.out详细信息表格.GetRow(this.out详细信息表格.FocusedRowHandle) as M详细信息;
            if (__绑定数据 == null)
            {
                return;
            }
            new F详细信息(__绑定数据).ShowDialog();
        }

        private void do启动次数_确定_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_日志路径))
            {
                return;
            }
            if (this.in启动次数_次数.Value <= 0 || this.in启动次数_次数.Value > _概要信息.启动次数)
            {
                XtraMessageBox.Show("次数超出范围", "失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var __启动次数 = (int)this.in启动次数_次数.Value;
            绑定单次启动(__启动次数);

            if (_分组信息列表.Count > 0)
            {
                var __绑定数据 = this.in启动次数_时段.SelectedIndex == 0 ? _分组信息列表.First() : _分组信息列表.Last();
                定位数据(__绑定数据);
            }
            else
            {
                var __绑定数据 = this.in启动次数_时段.SelectedIndex == 0 ? _详细信息列表.First() : _详细信息列表.Last();
                定位数据(__绑定数据);
            }
        }

        private void do指定时间_确定_Click(object sender, EventArgs e)
        {
            var __指定时间 = this.in指定时间_起始时间.Time;
            this.out详细信息列表.DataSource = null;
            this.out概要信息列表.DataSource = null;
            var __启动次数 = _概要信息.启动列表.FindIndex(q => q.时间 >= __指定时间);
            绑定单次启动(__启动次数);
            var __绑定数据 = _详细信息列表.Find(q => q.时间 >= __指定时间);
            定位数据(__绑定数据);
        }

        private void out警告信息表格_Click(object sender, EventArgs e)
        {
            if (this.out警告信息表格.FocusedRowHandle < 0)
            {
                return;
            }
            var __绑定数据 = this.out警告信息表格.GetRow(this.out警告信息表格.FocusedRowHandle) as M详细信息;
            var __启动次数 = _概要信息.启动列表.FindIndex(q => q.时间 >= __绑定数据.时间);
            绑定单次启动(__启动次数);
            定位数据(__绑定数据);
        }

        private void 定位数据(M详细信息 __绑定数据)
        {
            if (__绑定数据 == null)
            {
                return;
            }
            var __分组位置 = _分组信息列表.FindIndex(q => q.时间 > __绑定数据.时间) - 1;
            if (__分组位置 < 0)
            {
                __分组位置 = _分组信息列表.Count - 1;
            }
            this.out概要信息表格.FocusedRowHandle = this.out概要信息表格.GetRowHandle(__分组位置);
            this.out详细信息表格.FocusedRowHandle = this.out详细信息表格.GetRowHandle(__绑定数据.Id - _启动信息.Id);
        }

        private void 加载日志文件()
        {
            this.out详细信息列表.DataSource = null;
            this.out概要信息列表.DataSource = null;
            this.out警告信息列表.DataSource = null;
            this.out起始时间.Text = string.Empty;
            this.out启动次数.Text = string.Empty;
            this.out结束时间.Text = string.Empty;
            this.out警告数量.Text = string.Empty;
            this.in启动次数_次数.Value = 0;

            _日志路径 = this.out文件路径.Text.Trim();
            if (string.IsNullOrEmpty(_日志路径))
            {
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    _日志路径 = Environment.GetCommandLineArgs()[1];
                    this.out文件路径.Text = _日志路径;
                }
                else
                {
                    _日志路径 = U路径.获取绝对路径("详细信息.log");
                    if (!File.Exists(_日志路径))
                    {
                        _日志路径 = U路径.获取绝对路径("日志\\详细信息.log");
                        if (!File.Exists(_日志路径))
                        {
                            //XtraMessageBox.Show("请将'详细日志.log'复制到程序目录,然后刷新", "未找到日志文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _日志路径 = string.Empty;
                            return;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(_日志路径))
            {
                SplashScreenManager.ShowForm(this.ParentForm, typeof(DemoWaitForm), true, true, false);
                try
                {
                    this.out文件路径.Text = _日志路径;
                    var __目录路径 = Path.GetDirectoryName(_日志路径);
                    var __文件名称 = Path.GetFileName(_日志路径);
                    var __路径列表 = new DirectoryInfo(__目录路径).GetFiles(__文件名称 + ".*").ToList();
                    //__路径列表.Sort((m, n) => m.LastWriteTime.CompareTo(n.LastWriteTime));
                    _I文本分析.加载日志(__路径列表);
                    On加载事件(__路径列表);
                    try
                    {
                        _概要信息 = _I文本分析.初步分析();
                    }
                    catch (ArgumentException ex)
                    {
                        XtraMessageBox.Show(ex.Message, "文件分析错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _日志路径 = string.Empty;
                        return;
                    }
                    this.out起始时间.Text = _概要信息.起始时间.ToString("MM-dd HH:mm");
                    this.out启动次数.Text = _概要信息.启动次数.ToString();
                    this.out结束时间.Text = _概要信息.结束时间.ToString("MM-dd HH:mm");
                    this.out警告数量.Text = _概要信息.警告列表.Count.ToString();
                    this.out警告信息列表.DataSource = _概要信息.警告列表;
                    this.out警告信息表格.FocusedRowHandle = -1;
                    this.in启动次数_次数.Value = _概要信息.启动次数;
                    this.in指定时间_起始时间.Time = _概要信息.启动列表[0].时间;
                    this.out文件路径.ShowToolTips = true;
                    this.out文件路径.ToolTip = _日志路径;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("分析出错: " + ex.Message);
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
        }

        private void 绑定单次启动(int __启动次数)
        {
            if (__启动次数 < 1)
            {
                __启动次数 = _概要信息.启动列表.Count;
            }
            if (__启动次数 > _概要信息.启动列表.Count)
            {
                __启动次数 = _概要信息.启动列表.Count;
            }
            _详细信息列表 = _I文本分析.查询详细(__启动次数);
            //_分组信息列表 = _详细信息列表.FindAll(q => q.跟踪周期 != E跟踪周期.无 && q.跟踪标记 > 0);
            _分组信息列表 = _详细信息列表.FindAll(q => q.等级 <= TraceEventType.Information);
            _启动信息 = _概要信息.启动列表[__启动次数 - 1];
            //this.out概要信息列表.DataSource = _分组信息列表;
            this.out概要信息列表.DataSource = (from q in _分组信息列表
                                         select new M详细信息
                                                    {
                                                        Id = q.Id,
                                                        标题 = q.标题,
                                                        等级 = q.等级,
                                                        跟踪标记 = q.跟踪标记,
                                                        内容 = q.内容.Replace(Environment.NewLine, ""),
                                                        时间 = q.时间,
                                                        线程 = q.线程,
                                                    }).ToList();
            this.out详细信息列表.DataSource = _详细信息列表;
            this.out当前启动次数.Text = __启动次数.ToString();
            this.out单次开始时间.Text = _详细信息列表.First().时间.ToString("MM-dd HH:mm:ss");
            this.out单次结束时间.Text = _详细信息列表.Last().时间.ToString("MM-dd HH:mm:ss");
        }

        private void do刷新文件路径_Click(object sender, EventArgs e)
        {
            this.加载日志文件();
            this.do启动次数_确定.PerformClick();
        }

        private void do启动次数_最后一次_Click(object sender, EventArgs e)
        {
            this.in启动次数_次数.Value = _概要信息.启动次数;
            this.do启动次数_确定.PerformClick();
        }

        private void do启动次数_第一次_Click(object sender, EventArgs e)
        {
            this.in启动次数_次数.Value = 1;
            this.do启动次数_确定.PerformClick();
        }

        private void do启动次数_上一次_Click(object sender, EventArgs e)
        {
            if (this.in启动次数_次数.Value <= 1)
            {
                XtraMessageBox.Show("没有了", "失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.in启动次数_次数.Value = this.in启动次数_次数.Value - 1;
            this.do启动次数_确定.PerformClick();
        }

        private void do启动次数_下一次_Click(object sender, EventArgs e)
        {
            if (this.in启动次数_次数.Value >= _概要信息.启动次数)
            {
                XtraMessageBox.Show("没有了", "失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.in启动次数_次数.Value = this.in启动次数_次数.Value + 1;
            this.do启动次数_确定.PerformClick();
        }

        private void out概要信息表格_CustomFieldValueStyle(object sender, LayoutViewFieldValueStyleEventArgs e)
        {
            var __绑定数据 = this.out概要信息表格.GetRow(e.RowHandle) as M详细信息;
            if (__绑定数据 == null)
            {
                return;
            }
            if (e.Column.FieldName == "线程" && __绑定数据.线程 != _启动信息.线程 && __绑定数据.线程 != "UI")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Green;
            }
        }

        private void out详细信息表格_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            var __绑定数据 = out详细信息表格.GetRow(e.RowHandle) as M详细信息;
            if (__绑定数据 == null)
            {
                return;
            }
            if (__绑定数据.等级 <= TraceEventType.Warning)
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
                e.HighPriority = true;
                return;
            }

            if (__绑定数据.等级 <= TraceEventType.Information)
            {
                e.Appearance.BackColor = Color.SkyBlue;
            }
            if (__绑定数据.跟踪周期 != E跟踪周期.无 && __绑定数据.跟踪标记 > 0)
            {
                e.Appearance.BackColor = Color.SkyBlue;
            }
            if (__绑定数据.跟踪标记 < 0)
            {
                e.Appearance.BackColor = Color.LightGray;
            }
        }

        private void out详细信息表格_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var _当前行信息 = out详细信息表格.GetRow(e.RowHandle) as M详细信息;
            if (_当前行信息 == null)
            {
                return;
            }
            switch (e.Column.VisibleIndex)
            {
                case 1:
                    if (_当前行信息.线程 != _启动信息.线程 && _当前行信息.线程 != "UI")
                    {
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.BackColor = Color.Green;
                    }
                    break;
            }
        }

        public event Action<List<FileInfo>> 加载事件;

        protected virtual void On加载事件(List<FileInfo> obj)
        {
            var handler = 加载事件;
            if (handler != null) handler(obj);
        }
    }
}
