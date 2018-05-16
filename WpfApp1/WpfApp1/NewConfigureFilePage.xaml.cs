using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
        ObservableCollection<Info> infos = new ObservableCollection<Info>
            {
                
            };
        public NewConfigureFilePage()
        {
            InitializeComponent();
            DataGridForNew.ItemsSource = infos;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == true)
            {
                infos.Add(new Info { path = ofd.FileName, way = ""});
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridForNew.SelectedItem != null)
            {
                Info DRV = (Info)DataGridForNew.SelectedItem;
                String name = DRV.path;
                
                //删除infos里面的数据
                for(int i = 0; i < infos.Count; i++)
                {
                    if(infos[i].path.CompareTo(name) == 0)
                    {
                        infos.Remove(infos[i]);       
                        break;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<File> configFiles = new List<File>();
            for (int i = 0; i < infos.Count; i++)
            {
                FileInfo fileInfo = new FileInfo(infos[i].path);
                File file = new File(fileInfo.Name, fileInfo.Length, infos[i].way, fileInfo.LastWriteTime.ToString(), fileInfo.FullName);
                configFiles.Add(file);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            string currentPath = System.IO.Directory.GetCurrentDirectory(); ;
            sfd.InitialDirectory = @currentPath;
            //设置保存的文件的类型
            sfd.Filter = "INI配置文件|*.ini";
            if (sfd.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        for (int i = 0; i < configFiles.Count; i++)
                        {
                            sw.WriteLine(configFiles[i].FileName);
                            sw.WriteLine(configFiles[i].FileSize);
                            sw.WriteLine(configFiles[i].Hashcode);
                            sw.WriteLine(configFiles[i].UpdateMethod);
                            sw.WriteLine(configFiles[i].LastModified);
                            sw.WriteLine(configFiles[i].Path);
                            //sw.WriteLine("");
                        }
                    }
                }
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("取消保存");
            }

        }
    }

    public class Info
    {
        public String path { get; set; }
        public String way { get; set; }
    }
   
}
