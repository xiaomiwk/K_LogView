namespace Win
{
    partial class F查询提示
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.out辅助容器 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.do返回 = new DevExpress.XtraEditors.ButtonEdit();
            this.out标题 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.out辅助容器)).BeginInit();
            this.out辅助容器.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.do返回.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // out辅助容器
            // 
            this.out辅助容器.Controls.Add(this.pictureEdit1);
            this.out辅助容器.Controls.Add(this.do返回);
            this.out辅助容器.Controls.Add(this.out标题);
            this.out辅助容器.Dock = System.Windows.Forms.DockStyle.Fill;
            this.out辅助容器.Location = new System.Drawing.Point(0, 0);
            this.out辅助容器.Name = "out辅助容器";
            this.out辅助容器.Size = new System.Drawing.Size(881, 382);
            this.out辅助容器.TabIndex = 0;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::Win.Properties.Resources.查询示例;
            this.pictureEdit1.Location = new System.Drawing.Point(29, 66);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(819, 288);
            this.pictureEdit1.TabIndex = 82;
            // 
            // do返回
            // 
            this.do返回.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do返回.Location = new System.Drawing.Point(856, 5);
            this.do返回.Name = "do返回";
            this.do返回.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.do返回.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, ">", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.do返回.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.do返回.Size = new System.Drawing.Size(20, 16);
            this.do返回.TabIndex = 81;
            // 
            // out标题
            // 
            this.out标题.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.out标题.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(160)))), ((int)(((byte)(225)))));
            this.out标题.Location = new System.Drawing.Point(17, 21);
            this.out标题.Name = "out标题";
            this.out标题.Size = new System.Drawing.Size(64, 19);
            this.out标题.TabIndex = 80;
            this.out标题.Text = "查询示例";
            // 
            // F查询提示
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(881, 382);
            this.Controls.Add(this.out辅助容器);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F查询提示";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.out辅助容器)).EndInit();
            this.out辅助容器.ResumeLayout(false);
            this.out辅助容器.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.do返回.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl out辅助容器;
        private DevExpress.XtraEditors.ButtonEdit do返回;
        private DevExpress.XtraEditors.LabelControl out标题;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;

    }
}
