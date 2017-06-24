using Newtonsoft.Json;
using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.View;
using System.Net.NetworkInformation;
using Xamarin.Forms;

namespace SportMe.One
{
    public partial class App : Application
    {


        public const string SportMeURL = "https://sportsmeleads.herokuapp.com/api/";
        //public const string SportMeURL = "http://192.168.1.102:3000/api/";


        public App()
        {
            InitializeComponent();


            if (PreferencesOne.Instance.LembrarMe)
                MainPage = new LeadEditView();
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



        public static bool CheckInternetConnection()
        {

            try
            {
                bool isConnected = NetworkInterface.GetIsNetworkAvailable();
                return true;
            }
            catch (System.Net.WebException ex)
            {
                return false;
            }
        }


    }
}
