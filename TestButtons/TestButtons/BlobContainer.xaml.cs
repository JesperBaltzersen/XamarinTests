using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestButtons
{
    public partial class BlobContainer : ContentView
    {
        public static readonly BindableProperty SelectedGenderProperty = BindableProperty.Create(nameof(SelectedGender), typeof(Gender), typeof(BlobContainer), defaultBindingMode: BindingMode.TwoWay, propertyChanged: GenderChanged);
        public static readonly BindableProperty GenderChangedCommandProperty = BindableProperty.Create(nameof(GenderChangedCommand), typeof(ICommand), typeof(BlobContainer));

        public event EventHandler<Gender> OnGenderChanged;

        private static void GenderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var blobContainer = (BlobContainer)bindable;
            var gender = (Gender)newValue;
            blobContainer.SelectedGender = gender;
            if (gender == Gender.Neutral)
            {
                blobContainer.femaleSelected = false;
                blobContainer.maleSelected = false;
                return;
            }

            if (gender == Gender.Female)
                blobContainer.FemaleSelected = true;
            else
                blobContainer.MaleSelected = true;
        }

        public enum Gender { Neutral, Male, Female }

        public BlobContainer()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public ICommand GenderChangedCommand { get; set; }
        public Gender SelectedGender { get; set; }

        private ICommand maleButtonCommand;
        private ICommand femaleButtonCommand;
        private bool maleSelected;
        private bool femaleSelected;

        protected ICommand MaleButtonCommand
        {
            get
            {
                return maleButtonCommand = maleButtonCommand ?? new Command(() => MaleSelected = true);
            }
            set
            {
                maleButtonCommand = value;
            }
        }

        protected ICommand FemaleButtonCommand
        {
            get
            {
                return femaleButtonCommand = femaleButtonCommand ?? new Command(() => FemaleSelected = true);
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
                maleSelected = value;
                 femaleSelected = !value;
                SelectedGender = Gender.Male;
                OnGenderChanged?.Invoke(this, SelectedGender);
                //GenderChangedCommand?.Execute(SelectedGender);
            }
        }
        public bool FemaleSelected
        {
            get { return femaleSelected; }
            set
            {
                femaleSelected = value;
                maleSelected = !value;
                SelectedGender = Gender.Female;
                OnGenderChanged?.Invoke(this, SelectedGender);
            }
        }
    }
}
