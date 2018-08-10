using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Events;
using Prism.Commands;
using Opsis.WPF.ViewModels.Base;
using Opsis.Interfaces;
using Opsis.WPF.Events;

namespace Opsis.WPF.ViewModels.Main.UserControls
{
    public class LoggedOnUserViewModel : ViewModelBase
    {
        #region Properties
        private string loggedOnUser = "";
        public string LoggedOnUser
        {
            get { return loggedOnUser; }
            set { SetProperty(ref loggedOnUser, value); }
        }
        #endregion

        private readonly IUserService userService;

        public LoggedOnUserViewModel(IEventAggregator eventAggregator, IUserService userService)
            : base(eventAggregator)
        {
            this.userService = userService;
        }

        public void Loaded()
        {
            GetLoggedOnUserData();
        }

        private void GetLoggedOnUserData()
        {
            var name = userService.LoggedOnUser.Name;
            var surname = userService.LoggedOnUser.Surname;
            LoggedOnUser = name + " " + surname;
        }
    }
}
