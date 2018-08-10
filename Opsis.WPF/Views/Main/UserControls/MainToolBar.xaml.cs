using Opsis.WPF.ViewModels.Main.UserControls;
using System.Windows.Controls;

namespace Opsis.WPF.Views.Main.UserControls
{
    public partial class MainToolBar : UserControl
    {
        public MainToolBar()
        {
            InitializeComponent();

            var vm = DataContext as MainToolBarViewModel;
            Unloaded += (sender, e) => vm.UnsubscribeEvents();
        }
    }
}
