namespace WinformUDload
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpload = new DevComponents.DotNetBar.ButtonItem();
            this.btnDownload = new DevComponents.DotNetBar.ButtonItem();
            this.btnDownlaodMore = new DevComponents.DotNetBar.ButtonItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.RTxtMessage = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.FileName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.FileMd5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.FileSize = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LastModifyTime = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.btnClear = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRefresh,
            this.btnUpload,
            this.btnDownload,
            this.btnDownlaodMore,
            this.btnClear});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1197, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 4;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_ClickAsync);
            // 
            // btnUpload
            // 
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Text = "上传文件";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Text = "单个下载";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDownlaodMore
            // 
            this.btnDownlaodMore.Name = "btnDownlaodMore";
            this.btnDownlaodMore.Text = "批量下载";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.RTxtMessage);
            this.panelEx1.Controls.Add(this.superGridControl1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1197, 546);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            this.panelEx1.Text = "panelEx1";
            // 
            // RTxtMessage
            // 
            // 
            // 
            // 
            this.RTxtMessage.BackgroundStyle.Class = "RichTextBoxBorder";
            this.RTxtMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RTxtMessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.RTxtMessage.Location = new System.Drawing.Point(720, 0);
            this.RTxtMessage.Name = "RTxtMessage";
            this.RTxtMessage.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs18\\lang2052\\par\r\n}\r\n";
            this.RTxtMessage.Size = new System.Drawing.Size(477, 546);
            this.RTxtMessage.TabIndex = 7;
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.Black;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileName);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileMd5);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileSize);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.LastModifyTime);
            this.superGridControl1.Size = new System.Drawing.Size(1197, 546);
            this.superGridControl1.TabIndex = 6;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "文件名";
            this.FileName.Name = "FileName";
            this.FileName.Width = 200;
            // 
            // FileMd5
            // 
            this.FileMd5.DataPropertyName = "FileMd5";
            this.FileMd5.HeaderText = "MD5";
            this.FileMd5.Name = "FileMd5";
            this.FileMd5.Width = 220;
            // 
            // FileSize
            // 
            this.FileSize.DataPropertyName = "FileSize";
            this.FileSize.HeaderText = "大小";
            this.FileSize.Name = "FileSize";
            this.FileSize.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            this.FileSize.Width = 75;
            // 
            // LastModifyTime
            // 
            this.LastModifyTime.DataPropertyName = "LastModifyTime";
            this.LastModifyTime.HeaderText = "修改时间";
            this.LastModifyTime.Name = "LastModifyTime";
            this.LastModifyTime.Width = 150;
            // 
            // btnClear
            // 
            this.btnClear.Name = "btnClear";
            this.btnClear.Text = "日志清屏";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 573);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.Name = "Form1";
            this.Text = "";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.ButtonItem btnUpload;
        private DevComponents.DotNetBar.ButtonItem btnDownload;
        private DevComponents.DotNetBar.ButtonItem btnDownlaodMore;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileName;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileMd5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileSize;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LastModifyTime;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx RTxtMessage;
        private DevComponents.DotNetBar.ButtonItem btnClear;
    }
}

