using System;
using Prism;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;

namespace TestPrism.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private DelegateCommand _nextWithCommand;
        public DelegateCommand NextWithCommand => _nextWithCommand ?? new DelegateCommand(NextWithPage);

        private DelegateCommand _nextWithoutCommand;
        public DelegateCommand NextWithoutCommand => _nextWithoutCommand ?? new DelegateCommand(NextWithoutPage);
        
        public MainPageViewModel(INavigationService navigationSevice) : base(navigationSevice) { }

        private async void NextWithPage()
        {
            await navigationService.NavigateAsync("NavigationPage/SecondPage");
        }

        private async void NextWithoutPage()
        {
            await navigationService.NavigateAsync("SecondPage");
        }
        
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }
    }
}