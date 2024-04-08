// Util.cs 文件
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace WindowsBaselineAssistant
{
    internal class Util
    {
        /// <summary>
        /// 获取系统版本信息
        /// </summary>
        /// <returns>系统版本信息</returns>
        public static string GetOSVersion()
        {
            try
            {
                OperatingSystem os = Environment.OSVersion;
                string osVersion = os.Version.ToString();
                string servicePack = GetServicePackVersion();
                string osName = GetOperatingSystemName();

                return $"{osName} {osVersion} {servicePack}";
            }
            catch (Exception)
            {
                return "获取系统信息失败";
            }
        }

        private static string GetServicePackVersion()
        {
            try
            {
                // 从注册表中获取服务包版本信息
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("CSDVersion");
                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 处理异常情况
            }
            return string.Empty;
        }

        private static string GetOperatingSystemName()
        {
            try
            {
                // 从注册表中获取操作系统名称信息
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("ProductName");
                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 处理异常情况
            }
            return "Microsoft Windows";
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static string GetIPAddress()
        {
            string ipAddress = "";
            try
            {
                // 获取主机名
                string hostName = Dns.GetHostName();
                // 获取主机的 IP 地址列表
                IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

                // 遍历 IP 地址列表，找到一个 IPv4 地址并返回
                foreach (IPAddress addr in ipAddresses)
                {
                    if (addr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = addr.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // 处理异常情况
            }
            return ipAddress;
        }
    }
    }
