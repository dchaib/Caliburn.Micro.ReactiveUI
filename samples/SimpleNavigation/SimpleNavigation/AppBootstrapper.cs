using Caliburn.Micro;
using System.Windows;

namespace SimpleNavigation
{    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Start();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
