// Form1.cs 文件
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using test1;
using WindowsBaselineAssistant;

namespace BaselineCheckTool
{
    public partial class Form1 : Form
    {
        // 加固命令的字典
        private Dictionary<string, string> reinforcementCommands = new Dictionary<string, string>()
        {
            { "1.2.3用户口令周期策略", "net accounts /maxpwage:90" }, // 设置账户口令的生存期不长于 90 天，避免密码泄露
            { "1.2.4用户口令过期提醒", "reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v PasswordExpiryWarning /t REG_DWORD /d 10 /f" }, // 密码到期前提示用户更改密码，避免用户因遗忘更换密码而导致账户失效
            { "1.1.2删除或禁用系统无关用户", "net user Guest /active:no && net user administrator /active:no" },
            { "1.1.3开启屏幕保护程序", "reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v ScreenSaveTimeOut /t REG_DWORD /d 300 /f" },
            { "1.2.1用户口令复杂度策略", "4.bat" },
            { "1.2.2用户登录失败锁定", "3.bat" },
            { "1.2.5系统不显示上次登录用户名", "reg add \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DontDisplayLastUserName /t REG_DWORD /d 1 /f" },
            { "1.4.3删除默认路由配置", "route delete 0.0.0.0" },
        };

        // 在 Form1.cs 文件中的 Form1 类中的构造函数中调用：
        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            LoadSystemInformation();
            BaselineChecker.LoadBaselineCheckResults(dataGridViewResults);
            this.Load += Form1_Load;
            this.Resize += Form1_Resize; // 订阅 Resize 事件
        }


        // 初始化界面
        private void InitializeUI()
        {
            this.Font = new Font("Segoe UI", 10); // 设置字体
            this.BackColor = Color.FromArgb(240, 240, 240); // 设置背景颜色
            labelSystemVersion.ForeColor = Color.FromArgb(0, 120, 215); // 设置标签文字颜色
            labelHostIP.ForeColor = Color.FromArgb(0, 120, 215); // 设置标签文字颜色
            buttonCheck.BackColor = Color.FromArgb(46, 204, 113); // 设置按钮背景颜色
            buttonCheck.ForeColor = Color.White; // 设置按钮文字颜色
            buttonReinforce.BackColor = Color.FromArgb(231, 76, 60); // 设置按钮背景颜色
            buttonReinforce.ForeColor = Color.White; // 设置按钮文字颜色

            AdjustButtonPositions(); // 初始调整按钮位置
        }

        // 加载系统信息
        private void LoadSystemInformation()
        {
            // 获取系统版本信息并显示在标签上
            string systemVersion = Util.GetOSVersion();
            labelSystemVersion.Text = "系统版本: " + systemVersion;

            // 获取本机IP地址并显示在标签上
            string hostIP = Util.GetIPAddress();
            labelHostIP.Text = "主机IP: " + hostIP;
        }

        // 点击“检查”按钮的事件处理程序
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            string irrelevantUsersStatus = Test.CheckIrrelevantUsersStatus(@"secpol.cfg");

            UpdateDetectedValues(irrelevantUsersStatus); // 更新检测到的值，同时传递irrelevantUsersStatus参数
            UpdateDetectionResults(); // 更新检测结果

            MessageBox.Show("检测完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 执行1.bat文件的代码...
            string batFilePath = Path.Combine(Application.StartupPath, "1.bat");
            System.Diagnostics.Process.Start(batFilePath);
        }


        // 更新检测到的值  
        private void UpdateDetectedValues(string irrelevantUsersStatus)
        {
            string secpolFilePath = @"secpol.cfg"; // 安全策略文件路径

            // 更新最大密码年龄
            dataGridViewResults.Rows[0].Cells["columnDetectedValue"].Value = Test.ReadValueFromFile(secpolFilePath, "MaximumPasswordAge");

            // 更新密码到期提醒
            dataGridViewResults.Rows[1].Cells["columnDetectedValue"].Value = Test.ReadValueFromFile(secpolFilePath, "PasswordExpiryWarning");

            // 更新系统无关用户状态
            dataGridViewResults.Rows[2].Cells["columnDetectedValue"].Value = Test.CheckIrrelevantUsersStatus(secpolFilePath);
            // 更新屏幕保护程序超时时间
            dataGridViewResults.Rows[3].Cells["columnDetectedValue"].Value = Test.CheckScreenSaverTimeout(secpolFilePath);
            dataGridViewResults.Rows[4].Cells["columnDetectedValue"].Value = Test.CheckPasswordSettings(secpolFilePath);

            dataGridViewResults.Rows[5].Cells["columnDetectedValue"].Value = Test.CheckPasswordSettings1(secpolFilePath);
            dataGridViewResults.Rows[6].Cells["columnDetectedValue"].Value = Test.CheckPermanentRoute(secpolFilePath);

        }


        // 更新检测结果
        private void UpdateDetectionResults()
        {
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                string standardValue = row.Cells["columnStandardValue"]?.Value?.ToString();
                string detectedValue = row.Cells["columnDetectedValue"]?.Value?.ToString();

                if (standardValue == detectedValue)
                {
                    row.Cells["columnResult"].Value = "符合";
                }
                else
                {
                    row.Cells["columnResult"].Value = "不符合";
                }
            }
        }

        // 点击“加固”按钮的事件处理程序
        private void buttonReinforce_Click(object sender, EventArgs e)
        {
            if (ConfirmReinforce()) // 如果确认加固
            {
                ReinforceSelectedItems(); // 对已选择的项执行加固
                ClearReinforcedItems(); // 清除已加固的项
                MessageBox.Show("加固成功!", "加固成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 执行1.bat文件的代码...
                string batFilePath = Path.Combine(Application.StartupPath, "1.bat");
                System.Diagnostics.Process.Start(batFilePath);
            }
        }

        // 确认加固
        private bool ConfirmReinforce()
        {
            DialogResult result = MessageBox.Show("确定要对已选择的项执行加固吗？", "确认加固", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // 显示确认对话框
            return result == DialogResult.Yes; // 如果用户点击是，则返回 true
        }

        // 对已选择的项执行加固
        private void ReinforceSelectedItems()
        {
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                DataGridViewCheckBoxCell reinforceCell = (DataGridViewCheckBoxCell)row.Cells["columnReinforce"];
                bool reinforceChecked = reinforceCell?.Value != null && (bool)reinforceCell.Value; // 检查是否选择了加固

                if (reinforceChecked) // 如果选择了加固
                {
                    string itemName = row.Cells["columnName"].Value.ToString(); // 获取项名称
                    if (reinforcementCommands.ContainsKey(itemName)) // 如果命令字典包含该项
                    {
                        ExecuteCommand(reinforcementCommands[itemName]); // 执行对应的命令
                    }
                }
            }
        }

        // 清除已加固的项
        private void ClearReinforcedItems()
        {
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                DataGridViewCheckBoxCell reinforceCell = (DataGridViewCheckBoxCell)row.Cells["columnReinforce"];
                bool reinforceChecked = reinforceCell?.Value != null && (bool)reinforceCell.Value; // 检查是否选择了加固

                if (reinforceChecked) // 如果选择了加固
                {
                    reinforceCell.Value = false; // 清除选择
                }
            }
        }

        // 执行命令
        private void ExecuteCommand(string command)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "cmd.exe";
                processInfo.Arguments = "/c " + command;
                processInfo.CreateNoWindow = true; // 不创建新窗口
                processInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动进程

                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit(); // 等待命令执行完成
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行命令时出现错误: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 窗体大小变化事件处理程序
        private void Form1_Load(object sender, EventArgs e)
        {
            AdjustButtonPositions();
        }

        private void AdjustButtonPositions()
        {
            dataGridViewResults.Width = this.ClientSize.Width - 50;
            dataGridViewResults.Height = this.ClientSize.Height - dataGridViewResults.Top - 100;

            // 调整列宽度
            dataGridViewResults.Columns["columnDescription"].Width = dataGridViewResults.Width * 2 / 10;
            dataGridViewResults.Columns["columnRegistryPath"].Width = dataGridViewResults.Width * 2 / 10;
            dataGridViewResults.Columns["columnStandardValue"].Width = dataGridViewResults.Width / 10;
            dataGridViewResults.Columns["columnDetectedValue"].Width = dataGridViewResults.Width / 10;
            dataGridViewResults.Columns["columnResult"].Width = dataGridViewResults.Width / 10;
            dataGridViewResults.Columns["columnReinforce"].Width = dataGridViewResults.Width / 10;

            int buttonTop = dataGridViewResults.Bottom + 10;
            buttonCheck.Top = buttonTop;
            buttonReinforce.Top = buttonTop;
            buttonReinforce.Left = buttonCheck.Right + 10;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustButtonPositions();
            // 调整注意事项文本的位置
            labelReinforceNotice.Location = new Point(20, buttonCheck.Bottom + 10);
        }


        // DataGridView 单元格内容点击事件处理程序
        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果有必要，处理单元格内容点击事件
        }
    }
}