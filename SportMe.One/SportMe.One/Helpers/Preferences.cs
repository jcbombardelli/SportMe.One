using Newtonsoft.Json;
using SportMe.One.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SportMe.One.Helpers
{
    public class Preferences<E>
    {

        private const string FILE_NAME = "preferences.json";

        public object GetPreferences()
        {
            var fileLoadedInString = DependencyService.Get<IDeviceStorage>().ReadAsString(FILE_NAME);
            return JsonConvert.DeserializeObject<E>(fileLoadedInString);
        }

        public object SetPreferences(E preferences)
        {
            DependencyService.Get<IDeviceStorage>().SaveAsString(FILE_NAME, JsonConvert.SerializeObject(preferences));
            return GetPreferences();

        }

    }
}
