using Newtonsoft.Json;

namespace SportMe.One.Domain
{
    public class Lead
    {

        [JsonIgnore]
        public int ID { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string Setor { get; set; } = string.Empty;
        public string Idade { get; set; } = string.Empty;
        public string Nascimento { get; set; }
        public string Datahora { get; set; }
        public bool MascFeme { get; set; } = false;

        [JsonIgnore]
        public bool Sync { get; set; } = false;
    }
}
