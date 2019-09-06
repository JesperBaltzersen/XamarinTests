using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static TestButtons.BlobContainer;

namespace TestButtons
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new MainPageViewModel();
            BindingContext = this;
            blobContainer.OnGenderChanged += BlobContainer_OnGenderChanged;
        }

        private void BlobContainer_OnGenderChanged(object sender, Gender e)
        {
            Debug.WriteLine($"Gender changed to: {e}");
        }

        public void GenderChanged(object sender, Gender gender)
        {

        }
    }
}
