using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.IOC
{
    public interface IDeviceStorage
    {

        string ReadAsString(string filename);

        void SaveAsString(string filename, string content);

    }
}
