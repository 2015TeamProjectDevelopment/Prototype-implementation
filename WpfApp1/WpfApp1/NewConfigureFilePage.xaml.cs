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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// NewConfigureFile.xaml 的交互逻辑
    /// </summary>
    public partial class NewConfigureFilePage : Page
    {
        public NewConfigureFilePage()
        {
            InitializeComponent();
        }

        private void Check_All_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                //全部选中
            }
        }
    }

    public class UpdateStyle
    {
        private String path;
    }
}
