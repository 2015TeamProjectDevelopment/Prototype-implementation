using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// softwareSetting.xaml 的交互逻辑
    /// </summary>
    public partial class SoftwareSetting : Window
    {
        public SoftwareSetting()
        {
            InitializeComponent();
        }

        private void changePathButton_Click(object sender, RoutedEventArgs e)
        {
            //目录选择
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.pathText.Text = m_Dir;
        }

        private void changeURL_Click(object sender, RoutedEventArgs e)
        {
            //检查一下是否合法 
            if(! CheckUri(this.URLText.Text.Trim()))
            {
                System.Windows.MessageBox.Show("URL不合法", "ERROR");
            }
        }

        public static bool CheckUri(string strUri)
        {
            try
            {
                System.Net.HttpWebRequest.Create(strUri).GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void sureButton_Click(object sender, RoutedEventArgs e)
        {
            //确定 把路径和URL存入本地文件？
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
