using SportMe.One.Helpers;
using System.ComponentModel;


namespace SportMe.One.ViewModel.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        bool _bIsLoading = false;
        public bool IsLoading
        {
            get { return _bIsLoading; }
            set
            {
                if (_bIsLoading != value)
                {
                    _bIsLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
