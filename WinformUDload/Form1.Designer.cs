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
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.FileName = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.FileMd5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.FileSize = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.LastModifyTime = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
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
            this.btnDownlaodMore});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(964, 27);
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
            // 
            // btnDownlaodMore
            // 
            this.btnDownlaodMore.Name = "btnDownlaodMore";
            this.btnDownlaodMore.Text = "批量下载";
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.Black;
            this.superGridControl1.Location = new System.Drawing.Point(0, 27);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileName);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileMd5);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.FileSize);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.LastModifyTime);
            this.superGridControl1.Size = new System.Drawing.Size(964, 510);
            this.superGridControl1.TabIndex = 5;
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 537);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.bar1);
            this.Name = "Form1";
            this.Text = "";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.ButtonItem btnUpload;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileName;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileMd5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn FileSize;
        private DevComponents.DotNetBar.SuperGrid.GridColumn LastModifyTime;
        private DevComponents.DotNetBar.ButtonItem btnDownload;
        private DevComponents.DotNetBar.ButtonItem btnDownlaodMore;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

