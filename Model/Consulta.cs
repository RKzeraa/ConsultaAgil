using System.Data.Common;
using System.Text.Json;
using Newtonsoft.Json;

class Consulta
{
    private static int nextId = 1;
    public int Id { get; }
    public Paciente Paciente { get; set; } = new Paciente();
    public DateTime Dia { get; set; }
    public DateTime Hora { get; set; }
    public string Especialidade { get; set; } = string.Empty;

    internal Consulta()
    {
    }

    [JsonConstructor]
    internal Consulta(int id, Paciente paciente, string dia, string hora, string especialidade)
    {
        Id = id;
        Paciente = paciente;
        Dia = DateTime.Parse(dia);
        Hora = DateTime.Parse(hora);
        Especialidade = especialidade;
        nextId++;
    }
    internal Consulta(Paciente paciente, string dia, string hora, string especialidade)
    {
        Id = nextId;
        Paciente = paciente;
        Dia = DateTime.Parse(dia);
        Hora = DateTime.Parse(hora);
        Especialidade = especialidade;
        nextId++;
    }

    public bool JsonSerializar(Consulta consulta, string path)
    {
        string json = JsonConvert.SerializeObject(consulta, Formatting.Indented);
        return SaveFileConsulta(json, path);
    }

    public bool JsonSerializarLista(List<Consulta> listaConsulta, string path)
    {
        string json = JsonConvert.SerializeObject(listaConsulta, Formatting.Indented);
        return SaveFileConsulta(json, path);
    }

    private bool SaveFileConsulta(string json, string path)
    {
        try
        {
            using StreamWriter file = new(path);
            file.WriteLine(json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public static Consulta JsonDesserializar(string path)
    {
        string json = OpenFileConsulta(path);
        if (json[..5] != "Falha")
        {
            return JsonConvert.DeserializeObject<Consulta>(json)!;
        }
        else
        {
            return new();
        }
    }

    public static List<Consulta> JsonDesserializarLista(string path)
    {
        string json = OpenFileConsulta(path);
        if (json[..5] != "Falha")
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Consulta>>(json)!;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Consulta>();
            }
        }
        else
        {
            return new List<Consulta>();
        }
    }


    private static string OpenFileConsulta(string path)
    {
        try
        {   
            using StreamReader file = new(path);
            string json = file.ReadToEnd();
            return json;
        }
        catch (Exception ex)
        {
            return "Falha: " + ex.Message;
        }
    }

    public override string ToString()
    {
        return $"Consulta ID: {Id} \nPaciente: {Paciente} \nDia: {Dia:d} \nHora: {Hora:HH:mm} \nEspecialidade: {Especialidade}\n";
    }
}