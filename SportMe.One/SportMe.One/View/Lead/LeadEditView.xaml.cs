using SportMe.One.Domain;
using SportMe.One.Helpers;
using SportMe.One.ViewModel;
using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SportMe.One.View
{

    public partial class LeadEditView : ContentPage
    {

        private LeadEditViewModel leadEditViewModel = new LeadEditViewModel();
        private RestClient rc = new RestClient();



        public LeadEditView()
        {
            InitializeComponent();
            BindingContext = leadEditViewModel;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var url = string.Format("{0}{1}?club={2}", App.SportMeURL, "clubs/sectors", "Santos");

            var result = await rc.Request(url, RestClient.HTTPMethod.GET);

            string verfi = await result.Content.ReadAsStringAsync();
            verfi = verfi.Replace('\"', ' ');
            verfi = verfi.Replace('[', ' ');
            verfi = verfi.Replace(']', ' ');
            verfi = verfi.Trim();
            var p = verfi.Split(',');

            pickerLead.ItemsSource = p;




        }


        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            leadEditViewModel.Lead.Datahora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.CurrentUICulture);


            if (App.CheckInternetConnection())
            {
                try
                {

                    var result = await rc.Request("https://sportsmeleads.herokuapp.com/api/leads", RestClient.HTTPMethod.POST, leadEditViewModel.Lead);

                    if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Accepted)
                    {
                        leadEditViewModel.Lead.Sync = true;
                        await DisplayAlert("SportMe Leads", "Lead Adicionado e Sincronizado", "OK");
                    }
                    else
                    {
                        leadEditViewModel.Lead.Sync = false;
                        await DisplayAlert("SportMe Leads", "Lead Adicionado, mas não sincronizado, erro: " + result.StatusCode, "OK");

                    }

                }
                catch (Exception ex)
                {

                    await DisplayAlert("SportMe Leads", "Lead Adicionado sem sincronização.", "OK");
                }
            }

            leadEditViewModel.Lead = new Lead();
        }

        private async void Entry_Unfocused(object sender, FocusEventArgs e)
        {

            Entry entry = sender as Entry;


            if (!string.IsNullOrEmpty(entry.Text))
            {

                entry.Unfocused -= Entry_Unfocused;

                if (!Regex.IsMatch(entry.Text, "^((\\d*)[0-9])+[.][0-9]{2}"))
                {
                    await DisplayAlert("Formato Incorreto", "Verifique o valor do ingresso", "Vou verificar!");
                }

                entry.Unfocused += Entry_Unfocused;

            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                txtSex.Text = "Feminino";
            else
                txtSex.Text = "Masculino";
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var split = e.NewTextValue.Split('.');

            if (split.Length > 1 && split[1].Length > 2)
            {
                Entry entry = sender as Entry;
                entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
    }
}