
using System;
using Wtd.PCL.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wtd.PCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            try
            {
                BindingContext = new JobViewModel { Navigation = Navigation };
            }catch(Exception ex)
            {
                var cakes = ex.Message;
            }
        }
    }
}