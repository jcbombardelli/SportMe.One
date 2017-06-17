﻿using System;
using SportMe.One.ViewModel;

using Xamarin.Forms;

namespace SportMe.One.View
{


    public partial class LoginView : ContentPage
    {


        private LoginViewModel loginViewModel = new LoginViewModel();


        public LoginView()
        {

            BindingContext = new LoginViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            loginViewModel.GetClubs();

        }


    }
}