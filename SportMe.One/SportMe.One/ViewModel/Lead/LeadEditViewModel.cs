using Newtonsoft.Json;
using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.View;
using SportMe.One.ViewModel.Base;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SportMe.One.ViewModel
{
    public class LeadEditViewModel : BaseViewModel
    {


        #region Getters and Setters
        private Lead lead;
        public Lead Lead
        {
            get { return lead; }
            set
            {
                lead = value;
                OnPropertyChanged(nameof(Lead));
            }
        }

        #endregion



        public ICommand RegisterCommand { get; set; }


        public LeadEditViewModel() : base()
        {
            RegisterCommand = new Command(DoRegister);
            lead = new Lead();
        }



        private async void DoRegister()
        {
            lead.Datahora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.CurrentUICulture);


            if (App.CheckInternetConnection())
            {
                try
                {
                    var rc = new RestClient();
                    var result = await rc.Request("https://sportsmeleads.herokuapp.com/api/leads", RestClient.HTTPMethod.POST, Lead);

                    if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Accepted)
                    {
                        Lead.Sync = true;
                        //await DisplayAlert("SportMe Leads", "Lead Adicionado e Sincronizado", "OK");
                    }
                    else
                    {
                        Lead.Sync = false;
                        //await DisplayAlert("SportMe Leads", "Lead Adicionado, mas não sincronizado, erro: " + result.StatusCode, "OK");

                    }

                }
                catch (Exception ex)
                {

                    // await DisplayAlert("SportMe Leads", "Lead Adicionado sem sincronização.", "OK");
                }
            }

            Lead = new Lead();
        }

    }
}
