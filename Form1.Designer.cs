using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaselineCheckTool
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Form1_Load);
            this.labelReinforceNotice = new System.Windows.Forms.Label();
            this.components = new System.ComponentModel.Container();
            this.labelSystemVersion = new System.Windows.Forms.Label();
            this.labelHostIP = new System.Windows.Forms.Label();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRegistryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStandardValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDetectedValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnReinforce = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonReinforce = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSystemVersion
            // 
            this.labelSystemVersion.AutoSize = true;
            this.labelSystemVersion.Location = new System.Drawing.Point(16, 10);
            this.labelSystemVersion.Name = "labelSystemVersion";
            this.labelSystemVersion.Size = new System.Drawing.Size(107, 15);
            this.labelSystemVersion.TabIndex = 0;
            this.labelSystemVersion.Text = "系统版本: N/A";
            // 
            // labelHostIP
            // 
            this.labelHostIP.AutoSize = true;
            this.labelHostIP.Location = new System.Drawing.Point(16, 35);
            this.labelHostIP.Name = "labelHostIP";
            this.labelHostIP.Size = new System.Drawing.Size(93, 15);
            this.labelHostIP.TabIndex = 1;
            this.labelHostIP.Text = "主机IP: N/A";
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnDescription,
            this.columnRegistryPath,
            this.columnStandardValue,
            this.columnDetectedValue,
            this.columnResult,
            this.columnReinforce});
            this.dataGridViewResults.Location = new System.Drawing.Point(27, 62);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.RowHeadersWidth = 51;
            this.dataGridViewResults.Size = new System.Drawing.Size(936, 346);
            this.dataGridViewResults.TabIndex = 2;
            this.dataGridViewResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellContentClick);
            // 
            // columnName
            // 
            this.columnName.HeaderText = "检测名称";
            this.columnName.MinimumWidth = 6;
            this.columnName.Name = "columnName";
            this.columnName.Width = 150;
            // 
            // columnDescription
            // 
            this.columnDescription.HeaderText = "描述";
            this.columnDescription.MinimumWidth = 6;
            this.columnDescription.Name = "columnDescription";
            this.columnDescription.Width = 150;
            // 
            // columnRegistryPath
            // 
            this.columnRegistryPath.HeaderText = "查看方式(注册表路径/secedit命令/其他命令)";
            this.columnRegistryPath.MinimumWidth = 6;
            this.columnRegistryPath.Name = "columnRegistryPath";
            this.columnRegistryPath.Width = 150;
            // 
            // columnStandardValue
            // 
            this.columnStandardValue.HeaderText = "标准值";
            this.columnStandardValue.MinimumWidth = 6;
            this.columnStandardValue.Name = "columnStandardValue";
            this.columnStandardValue.Width = 150;
            // 
            // columnDetectedValue
            // 
            this.columnDetectedValue.HeaderText = "检测值";
            this.columnDetectedValue.MinimumWidth = 6;
            this.columnDetectedValue.Name = "columnDetectedValue";
            this.columnDetectedValue.Width = 150;
            // 
            // columnResult
            // 
            this.columnResult.HeaderText = "检测结果";
            this.columnResult.MinimumWidth = 6;
            this.columnResult.Name = "columnResult";
            this.columnResult.Width = 150;
            // 
            // columnReinforce
            // 
            this.columnReinforce.HeaderText = "加固";
            this.columnReinforce.MinimumWidth = 6;
            this.columnReinforce.Name = "columnReinforce";
            this.columnReinforce.Width = 150;
            // buttonCheck
            this.buttonCheck.Location = new System.Drawing.Point(20, 414);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(100, 27);
            this.buttonCheck.TabIndex = 3;
            this.buttonCheck.Text = "检测";
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);

            // labelReinforceNotice
            this.labelReinforceNotice.AutoSize = true;
            this.labelReinforceNotice.Location = new System.Drawing.Point(20, 414 + this.buttonCheck.Height + 10); // 放在检测按钮下方
            this.labelReinforceNotice.Size = new System.Drawing.Size(400, 15);
            this.labelReinforceNotice.ForeColor = System.Drawing.Color.Red;
            this.labelReinforceNotice.Text = "注意: 检查项如果加固失败，需要手动进行加固!";
            this.Controls.Add(this.labelReinforceNotice);
            // 
            // buttonReinforce
            // 
            this.buttonReinforce.Location = new System.Drawing.Point(128, 414);
            this.buttonReinforce.Name = "buttonReinforce";
            this.buttonReinforce.Size = new System.Drawing.Size(100, 27);
            this.buttonReinforce.TabIndex = 4;
            this.buttonReinforce.Text = "加固";
            this.buttonReinforce.Click += new System.EventHandler(this.buttonReinforce_Click);
    
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 496);
            this.Controls.Add(this.buttonReinforce);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.labelHostIP);
            this.Controls.Add(this.labelSystemVersion);
            this.Name = "Form1";
            this.Text = "Baseline Check Tool";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelSystemVersion;
        private System.Windows.Forms.Label labelHostIP;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRegistryPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStandardValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDetectedValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnResult;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnReinforce;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonReinforce;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelReinforceNotice;
    }
}

