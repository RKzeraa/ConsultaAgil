using Clinica.Interface;
using Clinica.Model;

namespace Clinica.Controller;

public class ConsultaController : IConsultaController
{
    //private static readonly List<Consulta> Consultas = new();
    private static readonly List<Consulta> ConsultasJson = Consulta.JsonDesserializarLista("consultas.json");
    // private PacienteController PacienteController = new PacienteController();
    public static bool ListarTodasConsultas()
    {
        if (ConsultasJson.Count == 0)
        {
            Console.WriteLine("Não há consultas cadastradas!");
            return false;
        }
        else
        {
            Console.WriteLine("Lista de consultas cadastradas: ");
            foreach (Consulta consulta in ConsultasJson)
            {
                Console.WriteLine(consulta);
            }
        }
        return true;
    }
    public void CancelarConsulta()
    {
        bool condition = true;
        do
        {
            ListarTodasConsultas();
            Console.WriteLine("\nDigite o ID da consulta que deseja remarcar: ");
            int id = int.Parse(Console.ReadLine()!);
            Consulta? consulta = ListarConsulta(id);
            if (consulta != null)
            {
                Console.Clear();
                Console.WriteLine("1 - Remarcar consulta");
                Console.WriteLine("2 - Cancelar consulta");
                Console.WriteLine("3 - Sair");
                Console.Write("\nEscolha uma das opções acima: ");
                string opcao = Console.ReadLine()!;
                switch (opcao)
                {
                    case "1":
                        condition = RemarcarConsulta(consulta);
                        break;
                    case "2":
                        ConsultasJson.Remove(consulta);
                        if (consulta.JsonSerializarLista(ConsultasJson, "consultas.json"))
                        {
                            Console.WriteLine("\nRemovendo do banco de dados...");
                        }
                        Console.WriteLine("\nSua consulta cancelada com sucesso!");
                        break;
                    case "3":
                        condition = false;
                        break;
                    default:
                        Console.WriteLine("\nOpção Invalida! Pressione qualquer tecla para voltar ao menu de cancelamento...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nConsulta não encontrada!");
            }
        }
        while (condition);
    }

    private static Consulta ListarConsulta(int id)
    {
        foreach (var consulta in ConsultasJson.Where(consulta => consulta.Id == id))
        {
            return consulta;
        }

        return new Consulta();
    }

    public static bool ConsultaJaExiste(string dia, string hora)
    {
        if (ConsultaRetroativa(dia, hora) == false)
        {
            return ConsultasJson.Any(consulta => consulta.Dia == DateTime.Parse(dia) && consulta.Hora == DateTime.Parse(hora));
        }
        else
        {
            Console.WriteLine("\nConsulta Retroativa!");
            return true;
        }
    }

    public static bool ConsultaRetroativa(string dia, string hora)
    {
        if (DateTime.Parse(dia).CompareTo(DateTime.Now.Date) > 0) return false;
        if (DateTime.Now.Hour > DateTime.Parse(hora).Hour) return true;
        return DateTime.Now.Minute >= DateTime.Parse(hora).Minute || true;
    }

    public void MarcarConsulta()
    {
        while (PacienteController.ListarTodosPacientes())
        {
            Console.WriteLine("Selecione o paciente: ");
            int paciente = int.Parse(Console.ReadLine()!);
            if (PacienteController.ListarPaciente(paciente) != null)
            {
                Console.WriteLine("Digite o dia (Dia/Mês/Ano): ");
                string dia = Console.ReadLine()!;
                Console.WriteLine("Digite a hora (Hora:Minuto): ");
                string hora = Console.ReadLine()!;
                Console.WriteLine("Digite a especialidade: ");
                string especialidade = Console.ReadLine()!;
                if (ConsultaJaExiste(dia, hora) == false)
                {
                    Consulta consulta = new(PacienteController.ListarPaciente(paciente)!, dia, hora, especialidade);
                    ConsultasJson.Add(consulta);
                    if (consulta.JsonSerializarLista(ConsultasJson, "consultas.json"))
                    {
                        Console.WriteLine("\nConsulta marcada com sucesso!");
                    }
                    else
                    {
                        ConsultasJson.Remove(consulta);
                        Console.WriteLine("\nFalha ao marcar consulta!");
                    }
                    break;
                }
            }
            else
            {
                Console.WriteLine("Paciente não encontrado!");
                break;
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar Marcar Consulta?...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static bool RemarcarConsulta(Consulta consulta)
    {
        Console.WriteLine("Digite o dia (Dia/Mês/Ano): ");
        string dia = Console.ReadLine()!;
        Console.WriteLine("Digite a hora (Hora:Minuto): ");
        string hora = Console.ReadLine()!;
        Console.WriteLine("Digite a especialidade: ");
        string especialidade = Console.ReadLine()!;
        if (ListarConsulta(consulta.Id) != null)
        {
            consulta.Dia = DateTime.Parse(dia);
            consulta.Hora = DateTime.Parse(hora);
            consulta.Especialidade = especialidade;
            if (consulta.JsonSerializarLista(ConsultasJson, "consultas.json"))
            {
                Console.WriteLine("\nConsulta remarcada com sucesso!");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            else
            {
                Console.WriteLine("\nFalha ao remarcar consulta!");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
        }
        else
        {
            Console.WriteLine("\nConsulta não existe!");
            Console.ReadKey();
            Console.Clear();
            return true;
        }
    }
}