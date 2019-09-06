using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace TestPrism.ViewModels
{
    public class SecondPageViewModel : BaseViewModel
    {
        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand => _goBackCommand ?? new DelegateCommand(GoBack);

        public SecondPageViewModel(INavigationService navigationSevice) : base(navigationSevice) { }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private async void GoBack()
        {
            await navigationService.GoBackAsync();
        }

    }
}

