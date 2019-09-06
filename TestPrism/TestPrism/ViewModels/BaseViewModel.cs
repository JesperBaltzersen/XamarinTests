using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace TestPrism.ViewModels
{
	//TODO: should we delete INavigationAware interface? Maybe INavigatedAware instead?
    public class BaseViewModel : BindableBase, IPageLifecycleAware, INavigatedAware, IInitialize
	{
        protected readonly INavigationService navigationService;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string _callStack = string.Empty;
        public string CallStack
        {
            get => _callStack;
            set => SetProperty(ref _callStack, value);
        }

        public BaseViewModel() { }

		public BaseViewModel(INavigationService navigationService)
		{
			this.navigationService = navigationService;
		}

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
		#endregion

        public virtual void Initialize(INavigationParameters parameters)
        {
            CallStack += AddToCallStackLabel();
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            CallStack += AddToCallStackLabel();
        }

        public virtual void OnAppearing()
        {
            CallStack += AddToCallStackLabel();
        }

		public virtual void OnDisappearing()
        {
            CallStack += AddToCallStackLabel();
        }

		public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            CallStack += AddToCallStackLabel();
        }

        string AddToCallStackLabel([CallerMemberName] string name = "")
        {
            return Environment.NewLine + name;
        }
    }
}