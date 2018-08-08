using Opsis.WPF.Views;
using System.Windows;
using Prism.Modularity;
using Autofac;
using Prism.Autofac;
using Opsis.WPF.Views.Login;
using Opsis.Interfaces;
using Opsis.Services;

namespace Opsis.WPF
{
    class Bootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<ManagementService>().As<IManagementService>();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            base.ConfigureContainerBuilder(builder);
        }
    }
}
