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

        public Gender SelectedGender { get; set; } = Gender.Male;

        private ICommand _buttonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return _buttonCommand = (Command)(_buttonCommand ?? new Command((o) => ChangeStuff((Gender)o)));
            }
            set
            {
                _buttonCommand = value;
            }
        }

        private void ChangeStuff(Gender gender)
        {
            if (gender == Gender.Male)
            {
                MaleSelected = true;
                FemaleSelected = false;
            }
            else if (gender == Gender.Female)
            {
                MaleSelected = false;
                FemaleSelected = true;
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