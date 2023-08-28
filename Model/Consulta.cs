using Newtonsoft.Json;
using Clinica.Helper;

namespace Clinica.Model;

class Consulta
{
    private static int _nextId = 1;
    public int Id { get; }
    public Paciente Paciente { get; set; } = new();
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
        _nextId++;
    }
    internal Consulta(Paciente paciente, string dia, string hora, string especialidade)
    {
        Id = _nextId;
        Paciente = paciente;
        Dia = DateTime.Parse(dia);
        Hora = DateTime.Parse(hora);
        Especialidade = especialidade;
        _nextId++;
    }

    public bool JsonSerializarLista(List<Consulta> listaConsulta, string path)
    {
        return JsonFileHelper.SerializeAndSave(listaConsulta, path);
    }

    public static List<Consulta> JsonDesserializarLista(string path)
    {
        return JsonFileHelper.DeserializeFromFile<Consulta>(path);
    }

    public override string ToString()
    {
        return $"Consulta ID: {Id} \nPaciente: {Paciente} \nDia: {Dia:d} \nHora: {Hora:HH:mm} \nEspecialidade: {Especialidade}\n";
    }
}