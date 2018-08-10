using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Events;
using Prism.Commands;
using Opsis.WPF.ViewModels.Base;
using Opsis.Interfaces;
using Opsis.WPF.Events;
using Opsis.WPF.Events.Main.UserControls.MainToolBar;

namespace Opsis.WPF.ViewModels.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Delegates
        public delegate void LogoutDelegate();
        public event LogoutDelegate LogoutEvent;

        public delegate void QuitApplicationDelegate();
        public event QuitApplicationDelegate QuitApplicationEvent;
        #endregion

        private readonly IUserService userService;

        public MainWindowViewModel(IEventAggregator eventAggregator, IUserService userService)
            : base(eventAggregator)
        {
            this.userService = userService;
        }

        public void Loaded()
        {
            SubscribeExceptionHandling();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            events.Add(
                eventAggregator.GetEvent<LogoutEvent>(),
                eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutEvent?.Invoke()));

            events.Add(
                eventAggregator.GetEvent<QuitApplicationEvent>(),
                eventAggregator.GetEvent<QuitApplicationEvent>().Subscribe(() => QuitApplicationEvent?.Invoke()));
        }
    }
}
