using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestButtons
{
    public partial class BlobContainer : ContentView
    {
        public static readonly BindableProperty SelectedGenderProperty = BindableProperty.Create(nameof(SelectedGender), typeof(Gender), typeof(BlobContainer), defaultBindingMode: BindingMode.TwoWay, propertyChanged: GenderChanged);
        public static readonly BindableProperty GenderChangedCommandProperty = BindableProperty.Create(nameof(GenderChangedCommand), typeof(ICommand), typeof(BlobContainer), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: CommandPropertyChanged);

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var blobContainer = (BlobContainer)bindable;
            blobContainer.GenderChangedCommand = (ICommand)newValue;
        }

        public event EventHandler<Gender> OnGenderChanged;

        public ICommand GenderChangedCommand { get { return (ICommand)GetValue(GenderChangedCommandProperty); } set { SetValue(GenderChangedCommandProperty, value); } }
        public Gender SelectedGender { get { return (Gender)GetValue(SelectedGenderProperty); } set { SetValue(SelectedGenderProperty, value); } }

        public enum Gender { Neutral, Male, Female }

        private static void GenderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var blobContainer = (BlobContainer)bindable;
            var gender = (Gender)newValue;
            blobContainer.SelectedGender = gender;
            blobContainer.SetGenderState(gender, true);
        }


        public BlobContainer()
        {
            //BindingContext = this;
            InitializeComponent();
            //femaleGenderButton.OnButtonClicked += FemaleGenderButton_OnButtonClicked;
            //maleGenderButton.OnButtonClicked += MaleGenderButton_OnButtonClicked;
        }

        private void MaleGenderButton_OnButtonClicked(object sender, EventArgs e)
        {
            if (SelectedGender != Gender.Male)
                MaleSelected = true;
        }

        private void FemaleGenderButton_OnButtonClicked(object sender, EventArgs e)
        {
            if (SelectedGender != Gender.Female)
                FemaleSelected = true;
        }

        private ICommand maleButtonCommand;
        private ICommand femaleButtonCommand;
        private bool maleSelected;
        private bool femaleSelected;

        public ICommand MaleButtonCommand
        {
            get
            {
                return maleButtonCommand = maleButtonCommand ?? new Command(() => MaleSelected = true, canExecute: () => MaleSelected == false);
            }
            set
            {
                maleButtonCommand = value;
            }
        }

        private ICommand FemaleButtonCommand
        {
            get
            {
                return femaleButtonCommand = femaleButtonCommand ?? new Command(() => FemaleSelected = true, canExecute: () => FemaleSelected == false);
            }
            set
            {
                femaleButtonCommand = value;
            }
        }

        public bool MaleSelected
        {
            get { return maleSelected; }
            set
            {
                SetGenderState(Gender.Male, value);
            }
        }

        public bool FemaleSelected
        {
            get { return femaleSelected; }
            set
            {
                SetGenderState(Gender.Female, value);
            }
        }

        private void SetGenderState(Gender gender, bool enabled)
        {
            var previousSelectedGender = SelectedGender;

            if (gender == Gender.Female)
            {
                maleSelected = !enabled;
                femaleSelected = enabled;
                maleGenderButton.IsSelected = false;
                femaleGenderButton.IsSelected = true;
            }
            else if (gender == Gender.Male)
            {
                maleSelected = enabled;
                femaleSelected = !enabled;
                maleGenderButton.IsSelected = true;
                femaleGenderButton.IsSelected = false;
            }
            else if (gender == Gender.Neutral)
            {
                maleSelected = false;
                femaleSelected = false;
                maleGenderButton.IsSelected = false;
                femaleGenderButton.IsSelected = false;
            }

            SelectedGender = gender;

            if (previousSelectedGender != gender)
            {
                OnGenderChanged?.Invoke(this, SelectedGender);
                GenderChangedCommand?.Execute(SelectedGender);
            }
        }
    }
}
