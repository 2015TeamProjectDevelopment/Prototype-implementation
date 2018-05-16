using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

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
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            //initList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            listView.Items.Clear();
            initList();//执行查询
        }

        public void initList()
        {
            string fileDir = Environment.CurrentDirectory;
            DirectoryInfo fileFold = new DirectoryInfo(fileDir);
            FileInfo[] files = fileFold.GetFiles(); //获取指定文件夹下的所有文件

            for (int i = 0; files != null && i < files.Length; i++)  //将文件信息添加到List里面  
            {
                try
                {
                    if (files[i].Extension == ".ini")   //挑选出符合条件的信息  
                    {
                        //FInfo finfo = new FInfo(files[i].FullName, files[i].Name, files[i].Extension);
                        //AddFile(finfo);
                        ConfigList config1 = new ConfigList(files[i].Name, files[i].LastWriteTime, true);
                        config1.ConfigFileHashCode = config1.GetHashCode();
                        listView.Items.Add(config1);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

            for (int i = 0; i < 20; i++)
            {
                
               // listView.Items.Add(new ConfigList("111","111","111",true));
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
