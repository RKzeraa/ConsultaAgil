using Clinica.Helper;
using Newtonsoft.Json;

namespace Clinica.Model;

class Paciente
{
    private static int _nextId = 1;

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
        Id = _nextId;
        Nome = nome;
        Telefone = telefone;
        _nextId++;
    }


    public bool JsonSerializarLista(List<Paciente> listaPaciente, string path)
    {
        return JsonFileHelper.SerializeAndSave(listaPaciente, path);
    }

    public static List<Paciente> JsonDesserializarLista(string path)
    {
        return JsonFileHelper.DeserializeFromFile<Paciente>(path);
    }


    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Telefone: {Telefone}";
    }

}