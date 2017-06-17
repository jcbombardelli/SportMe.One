using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.Domain
{
    public class Feeder
    {

      
        public string User { get; set; }
        public string Pass { get; set; }
        public string Club { get; set; }


        public Feeder(string user, string pass)
        {
            User = user;
            Pass = pass;
        }
    }
}
