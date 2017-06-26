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


        private void EntryDate_TextChanged(object sender, TextChangedEventArgs e)
        {

            Entry entry = sender as Entry;
            entry.TextChanged -= EntryDate_TextChanged;

            if (e.NewTextValue != null)
            {

                if (e.NewTextValue.Length > (e.OldTextValue == null ? 0 : e.OldTextValue.Length))
                {
                    //string last = e.NewTextValue.Substring(e.NewTextValue.Length - 1, 1);
                    char last = e.NewTextValue[e.NewTextValue.Length - 1];
                    if (last != 47)
                    {
                        if (e.OldTextValue != null && e.OldTextValue != string.Empty)
                        {
                            var dt = Regex.Replace(e.NewTextValue.ToString(), @"\D", "");
                            switch (dt.Length)
                            {
                                case 2:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}/", dt.Substring(0, 2));
                                    else
                                        entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                                    break;
                                case 3:
                                    if (last >= 48 && last <= 49)
                                        entry.Text = string.Format("{0}/{1}", dt.Substring(0, 2), dt.Substring(2, 1));
                                    else
                                        entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                                    break;
                                case 4:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}/{1}/", dt.Substring(0, 2), dt.Substring(2, 2));
                                    else
                                        entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                                    break;
                                case 9:
                                    entry.Text = string.Format("{0}/{1}/{2}", dt.Substring(0, 2), dt.Substring(2, 2), dt.Substring(4, 4));
                                    break;
                            };
                        }
                        else
                        {
                            if (!(Convert.ToInt32(last) >= 48 && Convert.ToInt32(last) <= 51))
                                entry.Text = string.Empty;
                        }

                    }
                }
            }
            entry.TextChanged += EntryDate_TextChanged;
        }

        private void EntryCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            entry.TextChanged -= EntryCPF_TextChanged;

            if (e.NewTextValue != null)
            {

                if (e.NewTextValue.Length > (e.OldTextValue == null ? 0 : e.OldTextValue.Length))
                {
                    //string last = e.NewTextValue.Substring(e.NewTextValue.Length - 1, 1);
                    char last = e.NewTextValue[e.NewTextValue.Length - 1];
                    if (last != 47)
                    {
                        if (e.OldTextValue != null && e.OldTextValue != string.Empty)
                        {
                            var dt = Regex.Replace(e.NewTextValue.ToString(), @"\D", "");
                            switch (dt.Length)
                            {

                                case 1:
                                case 2:

                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}", dt);
                                    break;
                                case 3:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.", dt.Substring(0, 3));
                                    break;

                                case 4:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}", dt.Substring(0, 3), dt.Substring(3, 1));
                                    break;
                                case 5:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}", dt.Substring(0, 3), dt.Substring(3, 2));
                                    break;

                                case 6:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.", dt.Substring(0, 3), dt.Substring(3, 3));
                                    break;

                                case 7:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.{2}", dt.Substring(0, 3), dt.Substring(3, 3), dt.Substring(6, 1));
                                    break;
                                case 8:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.{2}", dt.Substring(0, 3), dt.Substring(3, 3), dt.Substring(6, 2));
                                    break;

                                case 9:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.{2}-", dt.Substring(0, 3), dt.Substring(3, 3), dt.Substring(6, 3));
                                    break;

                                case 10:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.{2}-{3}", dt.Substring(0, 3), dt.Substring(3, 3), dt.Substring(6, 3), dt.Substring(9, 1));
                                    break;
                                case 11:
                                    if (last >= 48 && last <= 57)
                                        entry.Text = string.Format("{0}.{1}.{2}-{3}", dt.Substring(0, 3), dt.Substring(3, 3), dt.Substring(6, 3), dt.Substring(9, 2));
                                    break;
                                default:
                                    entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                                    break;

                            };
                        }
                        else
                        {
                            if (!(Convert.ToInt32(last) >= 48 && Convert.ToInt32(last) <= 51))
                                entry.Text = string.Empty;
                        }
                    }
                }
            }

            entry.TextChanged += EntryCPF_TextChanged;
        }


        private async void EntryCPF_Unfocused(object sender, FocusEventArgs e)
        {
            Entry entry = sender as Entry;
            entry.Unfocused -= Entry_Unfocused;


            string valor = entry.Text.Replace(".", "").Replace("-", "");


            if (valor.Length != 11)
            {
                await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                return;
            }

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;



            if (igual || valor == "12345678909")
            {
                await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                return;
            }


            int[] numeros = new int[11];



            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());



            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];



            int resultado = soma % 11;


            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                {
                    await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                    return;
                }
            }

            else if (numeros[9] != 11 - resultado)
            {
                await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                return;
            }


            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];



            resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)

                {
                    await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                    return;
                }

            }
            else
            {

                if (numeros[10] != 11 - resultado)
                {
                    await DisplayAlert("CPF Inválido", "Formato de CPF inserido é inválido", "Vou Corrigir!");
                    return;
                }
            }

            entry.Unfocused += Entry_Unfocused;

        }
    }

}