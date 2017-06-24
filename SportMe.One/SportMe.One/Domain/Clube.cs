using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.Domain
{
    public class Clube
    {

        public string Nome { get; set; }
        public string[] Setores { get; set; }



        public Clube()
        {

        }


        public Clube(string nome)
        {
            Nome = nome;
        }
    }
}
