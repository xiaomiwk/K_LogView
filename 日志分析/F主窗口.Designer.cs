namespace Win
{
    partial class F主窗口
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F主窗口));
            this.out标签列表 = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.out标签列表)).BeginInit();
            this.SuspendLayout();
            // 
            // out标签列表
            // 
            this.out标签列表.AppearancePage.Header.Options.UseTextOptions = true;
            this.out标签列表.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.out标签列表.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.out标签列表.CustomHeaderButtons.AddRange(new DevExpress.XtraTab.Buttons.CustomHeaderButton[] {
            new DevExpress.XtraTab.Buttons.CustomHeaderButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.out标签列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.out标签列表.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            this.out标签列表.Location = new System.Drawing.Point(0, 0);
            this.out标签列表.Name = "out标签列表";
            this.out标签列表.Size = new System.Drawing.Size(1246, 702);
            this.out标签列表.TabIndex = 0;
            this.out标签列表.TabPageWidth = 300;
            // 
            // F主窗口
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1246, 702);
            this.Controls.Add(this.out标签列表);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F主窗口";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   日志分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.out标签列表)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl out标签列表;


    }
}

