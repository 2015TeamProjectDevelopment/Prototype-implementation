using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// UpdateUI.xaml 的交互逻辑
    /// 检测到更新时候的弹出界面
    /// </summary>
    public partial class UpdateUI : Window
    {
        public UpdateUI()
        {
            InitializeComponent();
        }

        private void Button_Click_cancle(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
