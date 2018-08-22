using Caliburn.Micro.ReactiveUI;

namespace SimpleNavigation
{
    public class ShellViewModel : ReactiveConductor<object>
    {
        public ShellViewModel()
        {
            ShowPageOne();
        }

        public void ShowPageOne()
        {
            ActivateItem(new PageOneViewModel());
        }

        public void ShowPageTwo()
        {
            ActivateItem(new PageTwoViewModel());
        }
    }
}
