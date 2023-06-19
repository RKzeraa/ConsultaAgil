using Newtonsoft.Json;

class Paciente
{
    private static int nextId = 1;

    public int Id { get; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    internal Paciente()
    {
    }

    [JsonConstructor]
    internal Paciente(int id, string nome, string telefone)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
    }

    internal Paciente(string nome, string telefone)
    {
        Id = nextId;
        Nome = nome;
        Telefone = telefone;
        nextId++;
    }

    public bool JsonSerializar(Paciente paciente, string path)
    {
        string json = JsonConvert.SerializeObject(paciente, Formatting.Indented);
        return SaveFilePaciente(json, path);
    }

    public bool JsonSerializarLista(List<Paciente> listaPaciente, string path)
    {
        string json = JsonConvert.SerializeObject(listaPaciente, Formatting.Indented);
        return SaveFilePaciente(json, path);
    }

    private bool SaveFilePaciente(string json, string path)
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

    public static Paciente JsonDesserializar(string path)
    {
        string json = OpenFilePaciente(path);
        if (json[..5] != "Falha")
        {
            return JsonConvert.DeserializeObject<Paciente>(json)!;
        }
        else
        {
            return new();
        }
    }

    public static List<Paciente> JsonDesserializarLista(string path)
    {
        string json = OpenFilePaciente(path);
        if (json[..5] != "Falha")
        {
            return JsonConvert.DeserializeObject<List<Paciente>>(json)!;
        }
        else
        {
            List<Paciente> listaPaciente = new();
            return listaPaciente;
        }
    }


    private static string OpenFilePaciente(string path)
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
        return $"ID: {Id}, Nome: {Nome}, Telefone: {Telefone}";
    }

}