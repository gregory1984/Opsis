using System.Windows;
using Opsis.WPF.ViewModels.Login;

namespace Opsis.WPF.Views.Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            var vm = DataContext as LoginWindowViewModel;
            Loaded += (sender, e) => vm.Loaded();
            Unloaded += (sender, e) => vm.UnsubscribeEvents();
        }
    }
}
