// BaselineChecker.cs 文件
using System.Windows.Forms;
using test1;

namespace BaselineCheckTool
{
    public class BaselineChecker
    {
        public static void LoadBaselineCheckResults(DataGridView dataGridViewResults)
        {
            string secpolFilePath = @"secpol.cfg"; // 安全策略文件路径

            dataGridViewResults.Rows.Add("1.2.3用户口令周期策略", "设置账户口令的生存期不长于 90天，避免密码泄露", "secedit命令", "90", Test.ReadValueFromFile(secpolFilePath, "MaximumPasswordAge"), "", false);
            dataGridViewResults.Rows.Add("1.2.4用户口令过期提醒", "密码到期前提示用户更改密码，避免用户因遗忘更换密码而导致账户失效", "secedit命令", "10", Test.ReadValueFromFile(secpolFilePath, "PasswordExpiryWarning"), "", false);
            dataGridViewResults.Rows.Add("1.1.2删除或禁用系统无关用户", "删除、禁用或锁定与设备运行、维护等工作无关的账户，避免无关账户被黑客利用", "其他命令", "No", Test.CheckIrrelevantUsersStatus(secpolFilePath), "", false);
            dataGridViewResults.Rows.Add("1.1.3开启屏幕保护程序", "操作系统设置开启屏幕保护，并将时间设定为5分钟，避免非法用户使用系统", "HKEY_CURRENT_USER\\Control Panel\\Desktop", "0x12c", Test.CheckScreenSaverTimeout(secpolFilePath), "", false);
            dataGridViewResults.Rows.Add("1.2.1用户口令复杂度策略", "口令长度不小于8位，由字母、数字和特殊字符组成，不得与账户名相同，避免口令被暴力破解", "secedit命令", "MinimumPasswordLength: 8, PasswordComplexity: 1", Test.CheckPasswordSettings(secpolFilePath), "", false);
            dataGridViewResults.Rows.Add("1.2.2用户登录失败锁定", "配置当用户连续认证失败次数超过5次，锁定该用户使用的账户10分钟，避免账户被恶意用户暴力破解", "其他命令", "LockoutBadCount:5,LockoutDuration:10", Test.CheckPasswordSettings1(secpolFilePath), "", false);
            dataGridViewResults.Rows.Add("1.4.3删除默认路由配置", "主机禁止使用默认路由，避免利用默认路由探测网络", "其他命令", "1", Test.CheckPermanentRoute(secpolFilePath), "", false);

        }
    }
}
