using Newtonsoft.Json;
using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.View;

using Xamarin.Forms;

namespace SportMe.One
{
    public partial class App : Application
    {

#if RELEASE
        public const string SportMeURL = "https://sportsmeleads.herokuapp.com/api/";
#else
        public const string SportMeURL = "http://192.168.1.102:3000/api/";
#endif

        public App()
        {
            InitializeComponent();


            if (PreferencesOne.Instance.LembrarMe)
                MainPage = new MainPage();
            else
            {
                MainPage = new LoginView();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }



    }
}
