using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.View;
using SportMe.One.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SportMe.One.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {

        private Feeder feederInstance;
        private bool isRememberMe = false;
        private string[] clubes;



        public Feeder FeederInstance
        {
            get { return feederInstance; }
            set
            {
                feederInstance = value;
            }
        }
        public bool IsRememberMe
        {
            get { return isRememberMe; }
            set
            {
                if (isRememberMe != value)
                {
                    isRememberMe = value;
                }
            }
        }
        public string[] Clubes
        {
            get { return clubes; }
            set
            {
                if (clubes != value)
                {
                    clubes = value;
                }
            }
        }



        private RestClient restCliente = new RestClient();
        public ICommand LoginCommand { get; set; }


        public LoginViewModel() : base()
        {
            LoginCommand = new Command(DoLogin);
            feederInstance = new Feeder(string.Empty, string.Empty);
        }


  
        public async void GetClubs()
        {
            //var result = await restCliente.Request("", RestClient.HTTPMethod.GET);
            //clubes = result.Content;
        }


        private async void DoLogin()
        {
            IsLoading = true;


            var result =  await restCliente.Request("", RestClient.HTTPMethod.PUT, feederInstance );


            if (IsRememberMe)
            {
                PreferencesOne.Instance.LembrarMe = true;
                PreferencesOne.Instance.Clube = feederInstance.Club;
                PreferencesOne.Instance.Usuario = feederInstance.User;

                if(PreferencesOne.Instance.PrimeiroLogin == null)
                    PreferencesOne.Instance.PrimeiroLogin = DateTime.Now;

                PreferencesOne.Instance.Save();
                
            }

            IsLoading = false;

            Application.Current.MainPage = new MainPage();        
            
        }

    }
}
