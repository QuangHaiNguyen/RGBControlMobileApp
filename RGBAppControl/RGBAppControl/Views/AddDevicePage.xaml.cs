using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RGBAppControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDevicePage : ContentPage
    {
        public AddDevicePage()
        {
            InitializeComponent();
        }

        async void ToolbarItemCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void ToolbarItemSave_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}