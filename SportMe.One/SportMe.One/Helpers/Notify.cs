using System.ComponentModel;

namespace SportMe.One.Helpers
{
    public class Notify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static event PropertyChangedEventHandler StaticPropertyChanged;


        public static void OnPropertyChanged(object obj, string propertyName = null)
        {
            StaticPropertyChanged?.Invoke(obj, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
