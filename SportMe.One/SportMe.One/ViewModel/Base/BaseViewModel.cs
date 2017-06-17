using SportMe.One.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.ViewModel.Base
{
    public class BaseViewModel
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
                    Notify.OnPropertyChanged(this, nameof(IsLoading));
                }
            }
        }
    }
}
