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
        public Clube Clube { get; set; } = new Clube();


        public Feeder(string user, string pass)
        {
            User = user;
            Pass = pass;
        }
    }
}
