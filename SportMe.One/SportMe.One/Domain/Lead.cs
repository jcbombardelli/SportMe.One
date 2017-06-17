using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportMe.One.Domain
{
    public class Lead
    {

        [JsonIgnore]
        public int ID { get; set; }

        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public string valor { get; set; } = string.Empty;
        public string setor { get; set; } = string.Empty;
        public string idade { get; set; } = string.Empty;
        public string datahora { get; set; }

        [JsonIgnore]
        public bool Sync { get; set; } = false;
    }
}
