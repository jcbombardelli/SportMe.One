using SportMe.One.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.Helpers
{
    public class PreferencesOne
    {

        private DateTime primeiroLogin;
        private DateTime fechamentoApp;
        private string usuario;
        private bool lembrarMe = false;
        private string clube;


        public bool LembrarMe
        {
            get { return lembrarMe; }
            set { lembrarMe = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Clube
        {
            get { return clube; }
            set { clube = value; }
        }

        public DateTime PrimeiroLogin
        {
            get { return primeiroLogin; }
            set { primeiroLogin = value; }
        }




        private static PreferencesOne instance = (PreferencesOne)new Preferences<PreferencesOne>().GetPreferences();

        private PreferencesOne()
        { }

        public static PreferencesOne Instance
        {
            get
            {
                if (instance == null)
                    instance = (PreferencesOne)new Preferences<PreferencesOne>().SetPreferences(new PreferencesOne());

                return instance;
            }
        }

        public void Save()
        {
            new Preferences<PreferencesOne>().SetPreferences(this);
        }

        public void SaveApp()
        {
            fechamentoApp = DateTime.UtcNow;
            Save();

        }

        public void Reset()
        {
            instance = (PreferencesOne)new Preferences<PreferencesOne>().SetPreferences(new PreferencesOne());
        }


    }
}
