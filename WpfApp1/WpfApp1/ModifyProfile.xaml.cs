using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// ModifyProfile.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class ModifyProfile : Window
    {
        ObservableCollection<Info> infos = new ObservableCollection<Info>
            {
            };

        public ModifyProfile()
        {
            InitializeComponent();

            DataGridForChange.ItemsSource = GetFileMessage();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == true)
            {
                infos.Add(new Info { path = ofd.FileName, way = "" });
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridForChange.SelectedItem != null)
            {
                Info DRV = (Info)DataGridForChange.SelectedItem;
                String name = DRV.path;

                //删除infos里面的数据
                for (int i = 0; i < infos.Count; i++)
                {
                    if (infos[i].path.CompareTo(name) == 0)
                    {
                        infos.Remove(infos[i]);
                        break;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            //设置保存的文件的类型
            sfd.Filter = "INI配置文件|*.ini";
            if (sfd.ShowDialog() == true)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("取消保存");
            }
        }

        public ObservableCollection<Info> GetFileMessage()
        {
            string fileDir = Environment.CurrentDirectory;
     
            String fileDirResp = Read(fileDir+ "\\fileName.txt");
            DirectoryInfo fileFold = new DirectoryInfo(fileDir);
            FileInfo[] files = fileFold.GetFiles(); //获取指定文件夹下的所有文件

            for (int i = 0; files != null && i < files.Length; i++)  //将文件信息添加到List里面  
            {
                //fileDirResp = Encoding.UTF8.GetString(Encoding.Default.GetBytes(fileDirResp));
                if (files[i].Name == fileDirResp)   //挑选出符合条件的信息  
                {

                    IniFiles ini_file_read = new IniFiles(fileDir+"\\"+fileDirResp);
                    for(int j = 0; j < 10000; j++)
                    {
                        String tem_path = "session"+j.ToString();
                        String tem_file_path = ini_file_read.IniReadvalue(tem_path, "path");
                        String tem_file_updateMethod = ini_file_read.IniReadvalue(tem_path, "updateMethod");
                        
                        if(tem_file_path == "")
                        {
                            break;
                        }
                        infos.Add(new Info { path = tem_file_path, way = tem_file_updateMethod });
                    }
                    break;
                }
                 
            }
            return infos;
        }

        public String Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            String line;
            line = sr.ReadLine();
            return line;
        }

    }
}
