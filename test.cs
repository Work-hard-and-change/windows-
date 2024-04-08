// test.cs 文件
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace test1
{
    public class Test
    {
        public static string ReadValueFromFile(string filePath, string keyword)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains(keyword))
                    {
                        if (keyword == "PasswordExpiryWarning")
                        {
                            int commaIndex = line.IndexOf(',');
                            if (commaIndex != -1)
                            {
                                string valueString = line.Substring(commaIndex + 1).Trim();
                                return valueString;
                            }
                        }

                        else
                        {
                            string[] parts = line.Split('=');
                            if (parts.Length > 1)
                            {
                                return parts[1].Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
            }

            return "N/A";
        }

        public static string CheckIrrelevantUsersStatus(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("文件不存在: " + filePath);
                return "FileNotFound"; // 或者您可以返回null或其他适当的值  
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (trimmedLine.Contains("Yes"))
                    {
                        Console.WriteLine("文件中包含 Yes");
                        return "Yes";
                    }
                }
                // 如果没有找到"Yes"，返回"No"  
                return "No";
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取文件时发生错误: " + ex.Message);
                return "ErrorReadingFile"; // 或者您可以记录异常并返回null  
            }
        }

        public static string CheckScreenSaverTimeout(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains("ScreenSaveTimeOut"))
                    {
                        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 3)
                        {
                            return parts[parts.Length - 1].Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
            }

            return "N/A";
        }

        public static string CheckPasswordSettings(string filePath)
        {
            try
            {
                // 读取文件的所有行
                string[] lines = File.ReadAllLines(filePath);

                // 初始化变量以存储找到的值
                string minimumPasswordLength = "N/A";
                string passwordComplexity = "N/A";

                // 遍历文件的每一行
                foreach (string line in lines)
                {
                    // 检查是否包含关键字 "MinimumPasswordLength"
                    if (line.Contains("MinimumPasswordLength"))
                    {
                        // 解析行以获取值
                        string[] parts = line.Split('=');
                        if (parts.Length > 1)
                        {
                            minimumPasswordLength = parts[1].Trim();
                        }
                    }
                    // 检查是否包含关键字 "PasswordComplexity"
                    else if (line.Contains("PasswordComplexity"))
                    {
                        // 解析行以获取值
                        string[] parts = line.Split('=');
                        if (parts.Length > 1)
                        {
                            passwordComplexity = parts[1].Trim();
                        }
                    }
                }

                // 返回结果
                return $"MinimumPasswordLength: {minimumPasswordLength}, PasswordComplexity: {passwordComplexity}";
            }
            catch (Exception ex)
            {
                // 处理异常情况
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
                return "N/A";
            }
        }



        public static string CheckPasswordSettings1(string filePath)
        {
            try
            {
                // 读取文件的所有行
                string[] lines = File.ReadAllLines(filePath);

                // 初始化变量以存储找到的值
                string LockoutBadCount = "N/A";
                string LockoutDuration = "N/A";

                // 遍历文件的每一行
                foreach (string line in lines)
                {
                    // 检查是否包含关键字 "MinimumPasswordLength"
                    if (line.Contains("LockoutBadCount"))
                    {
                        // 解析行以获取值
                        string[] parts = line.Split('=');
                        if (parts.Length > 1)
                        {
                            LockoutBadCount = parts[1].Trim();
                        }
                    }
                    // 检查是否包含关键字 "PasswordComplexity"
                    else if (line.Contains("LockoutDuration"))
                    {
                        // 解析行以获取值
                        string[] parts = line.Split('=');
                        if (parts.Length > 1)
                        {
                            LockoutDuration = parts[1].Trim();
                        }
                    }
                }

                // 返回结果
                return $"LockoutBadCount:{LockoutBadCount},LockoutDuration:{LockoutDuration}";
            }
            catch (Exception ex)
            {
                // 处理异常情况
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
                return "N/A";
            }
        }


        public static string CheckDontDisplayLastUserName(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.Contains("DontDisplayLastUserName"))
                    {
                        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 3)
                        {
                            return parts[parts.Length - 1].Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
            }

            return "N/A";
        }




        public static string CheckPermanentRoute(string filePath)
        {
            try
            {
                // 使用GB2312编码读取文件  
                string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("GB2312"));
                foreach (string line in lines)
                {
                    // 检查是否包含 "0.0.0.0"  
                    if (line.Contains("0.0.0.0"))
                    {
                        return "0"; // 如果检测到 "0.0.0.0"，返回 "0"  
                    }
                }
                // 如果文件的所有行都不包含 "0.0.0.0"，则返回 "1"  
                return "1";
            }
            catch (Exception ex)
            {
                // 显示错误消息，并返回错误码  
                MessageBox.Show("读取文件时发生错误: " + ex.Message);
                return "error"; // 或者考虑抛出异常  
            }
        }























    }
}

