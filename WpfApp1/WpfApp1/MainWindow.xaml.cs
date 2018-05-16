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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        NewConfigureFilePage newConfigureFilePage = new NewConfigureFilePage();
        ConfigureFileListPage ConfigureFileListPage = new ConfigureFileListPage();
        UpdateSoftwarePage updateSoftwarePage = new UpdateSoftwarePage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(newConfigureFilePage);
        }

        private void MenuItem_Click_about_us(object sender, RoutedEventArgs e)
        {
            SoftwareSetting SWSetting = new SoftwareSetting();
            //在父窗口中间显示
            SWSetting.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SWSetting.Owner = this;
            SWSetting.Title = "基本设置";
            SWSetting.ShowDialog();
        }

        private void Button_Page_List(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(ConfigureFileListPage);
        }

        private void Button_Page_New(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(newConfigureFilePage);
        }

        private void Button_Page_Update(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(updateSoftwarePage);
        }

        private void MenuItem_Click_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    class ConfigList
    {
        private string configFileName;
        private string configFileModificationTime;
        private string configFileHashCode;
        private bool isVersion;

        public ConfigList(string configFileName, string configFileModificationTime, string configFileHashCode, bool isVersion)
        {
            this.configFileName = configFileName;
            this.configFileModificationTime = configFileModificationTime;
            this.configFileHashCode = configFileHashCode;
            this.isVersion = isVersion;
        }
    }

    //配置文件类
    class configFile
    {
        private List<File> configFiles;

        public configFile(List<File> configFiles)
        {
            this.ConfigFiles = configFiles;
        }

        internal List<File> ConfigFiles { get => configFiles; set => configFiles = value; }

        public override bool Equals(object obj)
        {
            var file = obj as configFile;
            return file != null &&
                   EqualityComparer<List<File>>.Default.Equals(ConfigFiles, file.ConfigFiles);
        }

        public override int GetHashCode()
        {
            return 2039249806 + EqualityComparer<List<File>>.Default.GetHashCode(ConfigFiles);
        }
    }

    //文件类
    class File
    {
        private String fileName;        //文件名
        private long fileSize;          //文件大小
        private int hashcode;           //哈希码
        private String updateMethod;    //更新操作
        private String lastModified;    //最后修改时间
        private String path;            //路径

        public File()
        {

        }

        //构造函数
        public File(string fileName, long fileSize, string updateMethod, string lastModified, string path)
        {
            this.fileName = fileName;
            this.fileSize = fileSize;
            this.hashcode = GetHashCode();
            this.updateMethod = updateMethod;
            this.lastModified = lastModified;
            this.path = path;
        }

        public string FileName { get => fileName; set => fileName = value; }
        public long FileSize { get => fileSize; set => fileSize = value; }
        public int Hashcode { get => hashcode; set => hashcode = value; }
        public string UpdateMethod { get => updateMethod; set => updateMethod = value; }
        public string LastModified { get => lastModified; set => lastModified = value; }
        public string Path { get => path; set => path = value; }

        public override bool Equals(object obj)
        {
            var file = obj as File;
            return file != null &&
                   fileName == file.fileName &&
                   fileSize == file.fileSize &&
                   hashcode == file.hashcode &&
                   updateMethod == file.updateMethod &&
                   lastModified == file.lastModified &&
                   path == file.path;
        }

        public override int GetHashCode()
        {
            var hashCode = 2096143202;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(fileName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(fileSize.ToString());
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(updateMethod);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastModified);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(path);
            return hashCode;
        }
    }

       
}
