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
    /// ConfigureFileListPage.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigureFileListPage : Page
    {
        public ConfigureFileListPage()
        {
            InitializeComponent();
            initList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void initList()
        {
            for (int i = 0; i < 20; i++)
            {
                
                listView.Items.Add(new ConfigList( "Name" , "time" + i, "hashcode",true));

            }
            //listView.DataContext = listBook;  
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ModifyProfile SWSetting = new ModifyProfile();
            SWSetting.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SWSetting.Title = "修改配置文件";
            SWSetting.ShowDialog();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
