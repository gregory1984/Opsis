﻿using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Events;
using Prism.Commands;
using Opsis.WPF.ViewModels.Base;
using Opsis.Interfaces;
using Opsis.WPF.Events;

namespace Opsis.WPF.ViewModels.Login
{
    public class LoginWindowViewModel : ViewModelBase
    {
        #region Delegates
        public delegate void UserLoggedOnDelegate();
        public event UserLoggedOnDelegate UserLoggedOnEvent;

        public delegate void UserBlockedDelegate();
        public event UserBlockedDelegate UserBlockedEvent;

        public delegate void WrongCredentialsDelegate();
        public event WrongCredentialsDelegate WrongCredentialsEvent;
        #endregion

        #region Properties
        private string username = "";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string versionNumber = "";
        public string VersionNumber
        {
            get { return versionNumber; }
            set { SetProperty(ref versionNumber, value); }
        }
        #endregion

        private readonly IDatabaseService databaseService;
        private readonly IUserService userService;

        public LoginWindowViewModel(IEventAggregator eventAggregator, IDatabaseService databaseService, IUserService userService)
            : base(eventAggregator)
        {
            this.databaseService = databaseService;
            this.userService = userService;
        }

        public void Loaded()
        {
            SubscribeExceptionHandling();
            ExecuteSafety(databaseService.Initialize);
            GetVersionNumber();
        }

        private async void GetVersionNumber()
        {
            try
            {
                VersionNumber = "v. " + await Task.Run(() => databaseService.GetCurrentVersionNumber());
            }
            catch (Exception ex) { eventAggregator.GetEvent<ExceptionOccuredEvent>().Publish(ex); }
        }

        private DelegateCommand<PasswordBox> login;
        public DelegateCommand<PasswordBox> Login
        {
            get => login ?? (login = new DelegateCommand<PasswordBox>(p =>
            {
                ExecuteSafety(() =>
                {
                    LoginState state = userService.Login(Username, p.Password);
                    switch (state)
                    {
                        case LoginState.LoggedOn: UserLoggedOnEvent?.Invoke(); break;
                        case LoginState.Blocked: UserBlockedEvent?.Invoke(); break;
                        case LoginState.WrongCredentials: WrongCredentialsEvent?.Invoke(); break;
                    }
                });
            }));
        }
    }
}
