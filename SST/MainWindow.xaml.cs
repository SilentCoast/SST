using SSTLib;
using System.Windows;

namespace SST
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel(new PointsSerializer(), "Resources/Points.xml");
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm.OnExit();
        }
    }
}
