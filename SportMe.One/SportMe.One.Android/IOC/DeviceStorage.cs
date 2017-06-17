using System;
using System.IO;
using Xamarin.Forms;
using SportMe.One.IOC;



[assembly: Dependency(typeof(SportMe.One.Droid.IOC.DeviceStorage))]
namespace SportMe.One.Droid.IOC
{
    public class DeviceStorage : IDeviceStorage
    {

        private string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


        public string ReadAsString(string filename)
        {
            var filePath = Path.Combine(documentPath, filename);

            if (!File.Exists(filePath))
                SaveAsString(filePath, string.Empty);

            return File.ReadAllText(filePath);
        }

        public void SaveAsString(string filename, string content)
        {

            var filePath = Path.Combine(documentPath, filename);
            File.WriteAllText(filePath, content);
        }
    }
}