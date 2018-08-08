using System.Windows;
using MahApps.Metro.Controls;
using Opsis.WPF.ViewModels.Login;
using Opsis.WPF.Helpers;

namespace Opsis.WPF.Views.Login
{
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();

            var vm = DataContext as LoginWindowViewModel;
            Loaded += (sender, e) => vm.Loaded();
            Unloaded += (sender, e) => vm.UnsubscribeEvents();

            vm.ExceptionOccuredEvent += (ex) => MessageBoxes.CriticalQuestion(ex.ToString());
            vm.WrongCredentialsEvent += () => MessageBoxes.Warning("Nieprawidłowa nazwa użytkownika lub hasło.");
            vm.UserBlockedEvent += () => MessageBoxes.Warning("Konto użytkownika jest zablokowane.");
            vm.UserLoggedOnEvent += () => { };
        }
    }
}
