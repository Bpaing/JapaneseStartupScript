using RoutineManager.MVVM.ViewModel;
using System.Windows;

namespace RoutineManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //BindingContext = new MainViewModel();
        }
    }
}
