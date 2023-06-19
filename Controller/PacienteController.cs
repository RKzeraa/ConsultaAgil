using System.Text.RegularExpressions;
internal class PacienteController : IPacienteController
{
    // internal static List<Paciente> Pacientes = new();
    private static readonly List<Paciente> PacientesJson = Paciente.JsonDesserializarLista("pacientes.json");

    public void CadastrarPaciente()
    {
        string telefone;
        string nome;
        do
        {
            Console.WriteLine("Digite o nome do paciente: ");
            nome = Console.ReadLine()!;
            do
            {
                Console.WriteLine("Digite o telefone do paciente: ");
                telefone = Console.ReadLine()!;
            }
            while (ValidarTelefone(telefone) == false);
        } while (PacienteJaExiste(telefone) != false);
        Paciente paciente = new(nome, telefone);
        PacientesJson.Add(paciente);
        if (paciente.JsonSerializarLista(PacientesJson, "pacientes.json"))
        {
            Console.WriteLine("\nPaciente cadastrado com sucesso!");
        }
        else
        {
            PacientesJson.Remove(paciente);
            Console.WriteLine("\nFalha ao cadastrar paciente!");
        }
    }

    public static bool ListarTodosPacientes()
    {
        if (PacientesJson.Count == 0)
        {
            Console.WriteLine("Não há pacientes cadastrados!");
            return false;
        }
        else
        {
            Console.WriteLine("Lista de pacientes cadastrados: ");
            foreach (Paciente paciente in PacientesJson)
            {
                Console.WriteLine(paciente);
            }
        }

        return true;
    }

    public static Paciente? ListarPaciente(int Id)
    {
        foreach (Paciente paciente in PacientesJson)
        {
            if (paciente.Id == Id)
            {
                return paciente;
            }
        }
        return null;
    }

    private static bool PacienteJaExiste(string telefone)
    {
        foreach (Paciente paciente in PacientesJson)
        {
            if (paciente.Telefone == telefone)
            {
                Console.WriteLine("Paciente já cadastrado! \nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
        }
        return false;
    }

    private static bool ValidarTelefone(string telefone)
    {
        Regex telefoneRegex = new("^\\+?[1-9][0-9]{7,14}$");
        if (telefoneRegex.IsMatch(telefone) == false)
            Console.WriteLine("Telefone inválido!");
        return telefoneRegex.IsMatch(telefone);
    }
}