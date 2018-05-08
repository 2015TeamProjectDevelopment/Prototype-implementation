using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

    public class methodList : List<String>
    {
        public methodList()
        {
            this.Add(" ");
            this.Add("新增");
            this.Add("替换");
            this.Add("删除");
        }
    }

    /// <summary>
    /// NewConfigureFile.xaml 的交互逻辑
    /// </summary>
    public partial class NewConfigureFilePage : Page
    {
        List<File> configureFile = new List<File>();
        public NewConfigureFilePage()
        {
            InitializeComponent();
            //DataContext = new NewConfigureFilePage();
            List<Info> infos = new List<Info>
            {
                new Info{path = "C:\\a", way = ""},
                new Info{path = "D:\\b", way = ""},
                new Info{path = "C:\\c", way = ""}
            };
            DataGridForNew.ItemsSource = infos;
        }

        private void Check_All_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                //全部选中
                
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
        }
    }

    public class Info
    {
        public String path { get; set; }
        public String way { get; set; }
    }
   
}
