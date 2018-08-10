using System.Windows.Controls;
using Opsis.WPF.ViewModels.Main.UserControls;

namespace Opsis.WPF.Views.Main.UserControls
{
    public partial class LoggedOnUser : UserControl
    {
        public LoggedOnUser()
        {
            InitializeComponent();

            var vm = DataContext as LoggedOnUserViewModel;
            Loaded += (sender, e) => vm.Loaded();
            Unloaded += (sender, e) => vm.UnsubscribeEvents();
        }
    }
}
