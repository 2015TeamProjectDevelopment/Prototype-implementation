using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private ConfigList cfPC;
        private ConfigList cfServer;
        private string downloadPath;
        string current = System.IO.Directory.GetCurrentDirectory();
        
        public UpdateUI(ConfigList cfPC, ConfigList cfServer)
        {
            InitializeComponent();
            this.cfPC = cfPC;
            this.cfServer = cfServer;
            showDiff();
            doUpdate();

        }

        //显示版本差异
        private void showDiff()
        {
            String sText = "";
            if(cfPC != null)
            {
                string name = cfPC.ConfigFileName;
                sText += "当前版本："+name.Substring(0, name.Length - 4) + "\n";
                sText += "哈希值：" + cfPC.ConfigFileHashCode + "\n";
            }
            if(cfServer != null)
            {
                string name = cfServer.ConfigFileName;
                sText += "\n" + "新版本：" + name.Substring(0, name.Length - 4) + "\n";
                sText += "哈希值：" + cfServer.ConfigFileHashCode + "\n";
            }
            textblock.Text = sText;
        }

        //执行更新操作
        private void download()
        {
            //下载的 源文件夹 url 和 下载后的本地文件夹名字
            StreamReader sr = new StreamReader(current+"//url.txt", Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                Uri uriAddress = new Uri(line);
                current = uriAddress.LocalPath;
            }

            string subFolderName = cfServer.ConfigFileName.Replace('.', '_'); //文件夹名字
            string srcPath = System.IO.Path.Combine(current + "//versionFolder", subFolderName);  //源文件夹
            Console.WriteLine(srcPath);
            if (!System.IO.Directory.Exists(srcPath))
            {
                MessageBox.Show("源文件夹不存在", "ERROR");
                return;
            }
            string PcPath = current + "\\PC";
            string destPath = System.IO.Path.Combine(PcPath, subFolderName); // 目标文件夹
            downloadPath = destPath;
            if (!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }

            //下载
            CopyDirectory(srcPath, destPath);
        }

        //下载操作 这里只是 复制复制复制。。
        private void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        System.IO.File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }


        }

        //更新操作
        private void doUpdate()
        {
            download();
            //读取配置文件并进行操作
            DirectoryInfo fileFold = new DirectoryInfo(downloadPath);
            FileInfo[] files = fileFold.GetFiles();
            string PcPath = current + "\\PC";
            string IniName = "";
            for (int i = 0; files != null && i < files.Length; i++)  //将文件信息添加到List里面  
            {
                if (files[i].Extension == ".ini")   //挑选出符合条件的信息  
                {
                    IniName = files[i].Name;
                    IniFiles ini_file_read = new IniFiles(downloadPath + "\\" + files[i].Name);
                    for (int j = 0; j < 10000; j++)
                    {
                        String tem_path = "session" + j.ToString();
                        String tem_file_name = ini_file_read.IniReadvalue(tem_path, "fileName");
                        String tem_file_updateMethod = ini_file_read.IniReadvalue(tem_path, "updateMethod");
                        if (tem_file_updateMethod == "")
                        {
                            break;
                        }
                        else if(tem_file_updateMethod == "新增")
                        {
                            System.IO.File.Copy(downloadPath + "\\" + tem_file_name,
                                PcPath + "\\" + tem_file_name, true);
                        }
                        else if (tem_file_updateMethod == "删除")
                        {
                            System.IO.File.Delete(PcPath + "\\" + tem_file_name);
                        }
                        else if (tem_file_updateMethod == "替换")
                        {
                            //先删除后复制
                            System.IO.File.Delete(PcPath + "\\" + tem_file_name);
                            System.IO.File.Copy(downloadPath + "\\" + tem_file_name,
                                PcPath + "\\" + tem_file_name, true);
                        }
                    }
                    break;
                }
            }

            //更新PC的ini
            DirectoryInfo Fold = new DirectoryInfo(PcPath);
            FileInfo[] fs = Fold.GetFiles();
            for (int i = 0; fs != null && i < fs.Length; i++)  //将文件信息添加到List里面  
            {
                if (fs[i].Extension == ".ini")   //挑选出符合条件的信息  
                {
                    Console.WriteLine(PcPath + "\\" + fs[i].Name);
                    //删除原来的
                    System.IO.File.Delete(PcPath + "\\" + fs[i].Name);
                    break;
                }
            }
            //复制新的进去
            System.IO.File.Copy(downloadPath + "\\" + IniName,
                PcPath + "\\" + IniName, true);

        }

        //取消更新
        private void Button_Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
