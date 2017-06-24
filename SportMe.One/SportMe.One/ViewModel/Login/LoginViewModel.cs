using Newtonsoft.Json;
using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.View;
using SportMe.One.ViewModel.Base;
using System;
using System.Linq;
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
                    OnPropertyChanged(nameof(Clubes));
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



        public async Task<string[]> GetClubs()
        {
            var result = await restCliente.Request(string.Format("{0}{1}", App.SportMeURL, "clubs"), RestClient.HTTPMethod.GET);

            var c = await result.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<Club[]>(c);

            Clubes =  a.Select(x => x.Clube).ToArray();

            return clubes;


        }



        private async void DoLogin()
        {
            IsLoading = true;
            //http://192.168.1.102:3000/api/feeder/verify?user=jcbombardelli&pass=avaiana1

            var url = string.Format("{0}{1}?user={2}&pass={3}", App.SportMeURL, "feeder/verify", feederInstance.User, feederInstance.Pass);

            var result = await restCliente.Request(url, RestClient.HTTPMethod.GET);

            string verfi = await result.Content.ReadAsStringAsync();
            bool check = Convert.ToBoolean(verfi);

            if (check)
            {
                if (IsRememberMe)
                {
                    PreferencesOne.Instance.LembrarMe = true;
                    PreferencesOne.Instance.Clube = feederInstance.Clube.Nome;
                    PreferencesOne.Instance.Usuario = feederInstance.User;

                    if (PreferencesOne.Instance.PrimeiroLogin == null)
                        PreferencesOne.Instance.PrimeiroLogin = DateTime.Now;

                    PreferencesOne.Instance.Save();

                }
            }


          
            IsLoading = false;

            Application.Current.MainPage = new LeadEditView();

        }

    }
}


public class Club
{
    public string Clube { get; set; }
}