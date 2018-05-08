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
    /// UpdateSoftwarePage.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateSoftwarePage : Page
    {
        public UpdateSoftwarePage()
        {
            InitializeComponent();
            //CommandBinding bindingNew = new CommandBinding(ApplicationCommands.New);
            //bindingNew.Executed += NewCommand;
            //this.CommandBindings.Add(bindingNew);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI SWSetting = new UpdateUI();
            //在父窗口中间显示
            SWSetting.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //SWSetting.Owner = this;
            SWSetting.Title = "软件更新";
            SWSetting.ShowDialog();
        }
    }
}
