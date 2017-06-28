
using Xamarin.Forms;

namespace Wtd.PCL.Helpers
{
    public static class AppHelper
    {
        public static Page CurrentPage()
        {
            var actionPage = Application.Current.MainPage;
            if (actionPage.Navigation != null)
            {
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;
                actionPage = navigationStack[
                    navigationStack.Count - 1];
            }

            return actionPage;

        }
    }
}
