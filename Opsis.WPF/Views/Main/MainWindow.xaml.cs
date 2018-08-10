using System.Windows;
using System.ComponentModel;
using MahApps.Metro.Controls;
using Opsis.WPF.Views.Login;
using Opsis.WPF.ViewModels.Main;
using Opsis.WPF.Helpers;

namespace Opsis.WPF.Views.Main
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = DataContext as MainWindowViewModel;
            Loaded += (sender, e) => vm.Loaded();
            Unloaded += (sender, e) => vm.UnsubscribeEvents();
            Closing += MainWindow_Closing;

            vm.ExceptionOccuredEvent += (ex) => MessageBoxes.CriticalQuestion(ex.ToString());
            vm.QuitApplicationEvent += () => Close();

            vm.LogoutEvent += () =>
            {
                var result = MessageBox.Show("Czy na pewno chcesz się wylogować?", "",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Closing -= MainWindow_Closing;
                    new LoginWindow().Show();
                    Close();
                }
            };
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno zamknąć aplikację?", "",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
