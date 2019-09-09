using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestButtons
{
    public class GNGenderButton : Grid
    {

        public event EventHandler OnButtonClicked;

        private readonly int haloDiameter = 120;
        private int HaloRadius { get => haloDiameter / 2; }

        private readonly int topButtonDiameter = 96;
        private int TopRadius { get => topButtonDiameter / 2; }

        private double NormalScale = 1.0;
        private double ScaleOutFactor { get => (double)haloDiameter / (double)topButtonDiameter; }

        BoxView genderButtonHalo;
        Button genderButtonTop;

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(GNGenderButton), false, BindingMode.OneWay, propertyChanged: OnIsSelectedChanged);
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var gNGenderButton = (GNGenderButton)bindable;
            gNGenderButton.SetButtonState();
        }

        public static readonly BindableProperty GNButtonTextProperty = BindableProperty.Create(nameof(GNButtonText), typeof(string), typeof(GNGenderButton), defaultValue: "", propertyChanged: OnButtonTextChanged);
        public string GNButtonText
        {
            get => (string)GetValue(GNButtonTextProperty);
            set => SetValue(GNButtonTextProperty, value);
        }
        private static void OnButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (GNGenderButton)bindable;
            button.genderButtonTop.Text = (string)newValue;
        }

        public static readonly BindableProperty GenderButtonClickedProperty = BindableProperty.Create(nameof(GenderButtonClicked), typeof(ICommand), typeof(GNGenderButton), propertyChanged: OnGenderButtonClickedCommand);
        public ICommand GenderButtonClicked
        {
            get => (ICommand)GetValue(GenderButtonClickedProperty);
            set
            {
                SetValue(GenderButtonClickedProperty, value);
            }
        }
        private static void OnGenderButtonClickedCommand(BindableObject bindable, object oldValue, object newValue)
        {
            ((GNGenderButton)bindable).genderButtonTop.Command = newValue as ICommand;
        }


        public GNGenderButton()
        {
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            genderButtonHalo = new BoxView
            {
                HeightRequest = topButtonDiameter,
                WidthRequest = topButtonDiameter,
                CornerRadius = TopRadius,
                //Style = (Style)Application.Current.Resources["GenderButtonHalo"],
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = true,
                BackgroundColor = Color.Black
            };

            genderButtonTop = new Button
            {
                HeightRequest = topButtonDiameter,
                WidthRequest = topButtonDiameter,
                CornerRadius = TopRadius,
                //Style = (Style)Application.Current.Resources["GenderButtonDeselected"],
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = true,
                BackgroundColor = Color.Bisque,
                Text = GNButtonText
            };

            genderButtonTop.Clicked += GenderButtonTop_Clicked;

            Children.Add(genderButtonHalo, 0, 0);
            Children.Add(genderButtonTop, 0, 0);
        }

        private void GenderButtonTop_Clicked(object sender, EventArgs e)
        {
            OnButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void SwitchSelected(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private async void SetButtonState()
        {
            
               if (!IsSelected)
               {
                   await genderButtonHalo.ScaleTo(NormalScale, easing: Easing.BounceOut);
                   //Todo: fade to deselected color
               }
               else
               {
                   await genderButtonHalo.ScaleTo(ScaleOutFactor, easing: Easing.BounceOut);
                   //Todo: fade to selected color
               }
           
        }
    }
}
