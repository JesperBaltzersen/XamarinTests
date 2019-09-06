using System;
using System.Windows.Input;
using Xamarin.Forms;
using static TestButtons.BlobContainer;

namespace TestButtons
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel()
        {
        }

        private ICommand _buttonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return _buttonCommand = (Command)(_buttonCommand ?? new Command(() => ChangeStuff()));
            }
            set
            {
                _buttonCommand = value;
            }
        }

        private void ChangeStuff()
        {
            if (MaleSelected)
            {
                MaleSelected = false;
                FemaleSelected = true;
            }
            else
            {
                MaleSelected = true;
                FemaleSelected = false;
            }
        }

        private bool _maleSelected;
        public bool MaleSelected
        {
            get => _maleSelected;
            set
            {
                SetProperty<bool>(ref _maleSelected, value);
            }
        }

        private bool _femaleSelected;
        public bool FemaleSelected
        {
            get => _femaleSelected;
            set
            {
                SetProperty<bool>(ref _femaleSelected, value);
            }
        }
    }
}