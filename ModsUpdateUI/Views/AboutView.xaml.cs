using MahApps.Metro.Controls;
using ModsUpdateUI.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace ModsUpdateUI.Views
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : MetroWindow
    {
        public AboutView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public List<Dependency> Dependencies { get; set; } = ConfigurationManager.SoftwareInformation.Dependencies.ToList();

        public SoftwareInfo MataInfo { get; set; } = ConfigurationManager.SoftwareInformation;

        private void HomePageButton_Click(object sender, System.Windows.RoutedEventArgs e) => System.Diagnostics.Process.Start(MataInfo.HomePage.ToString());
    }
}
