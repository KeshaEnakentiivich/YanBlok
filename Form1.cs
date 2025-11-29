using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace YanBlok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            notifyIcon1.ShowBalloonTip(10000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey explorer = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    explorer.SetValue("DisallowRun", 1, RegistryValueKind.DWord);
                }

                using (RegistryKey disallow = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    int index = 1;
                    disallow.SetValue(index.ToString(), "yandex.exe");
                    index++;

                    // Проверяем галку
                    if (checkBox1.Checked)
                    {
                        disallow.SetValue(index.ToString(), "browser.exe");
                    }
                }

                MessageBox.Show("Блокировка установлена. Выйдите и войдите снова в Windows.", "Готово");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.CurrentUser.DeleteSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun", false);
                using (RegistryKey explorer = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true))
                {
                    explorer.DeleteValue("DisallowRun", false);
                }

                MessageBox.Show("Блокировка снята.", "Готово");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/@uxthemerdll");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
