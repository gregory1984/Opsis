using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Events;
using Prism.Commands;
using Opsis.WPF.ViewModels.Base;
using Opsis.Interfaces;
using Opsis.WPF.Events;
using Opsis.WPF.Events.Main.UserControls.MainToolBar;

namespace Opsis.WPF.ViewModels.Main.UserControls
{
    public class MainToolBarViewModel : ViewModelBase
    {
        public MainToolBarViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {

        }

        private DelegateCommand logout;
        public DelegateCommand Logout
        {
            get => logout ?? (logout = new DelegateCommand(()
                => eventAggregator.GetEvent<LogoutEvent>().Publish()));
        }

        private DelegateCommand quitApplication;
        public DelegateCommand QuitApplication
        {
            get => quitApplication ?? (quitApplication = new DelegateCommand(()
                => eventAggregator.GetEvent<QuitApplicationEvent>().Publish()));
        }
    }
}
