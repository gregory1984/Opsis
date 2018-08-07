using Opsis.WPF.ViewModels.Base;
using Opsis.Interfaces;

namespace Opsis.WPF.ViewModels.Login
{
    public class LoginWindowViewModel : ViewModelBase
    {
        private readonly IDatabaseService databaseService;

        public LoginWindowViewModel(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void Loaded()
        {
            databaseService.Initialize();
        }
    }
}
