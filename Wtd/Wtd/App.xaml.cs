
using Xamarin.Forms;

namespace Wtd
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new Wtd.MainPage();
            MainPage = new NavigationPage(new PCL.Views.CalendarPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
