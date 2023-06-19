using System.Data;
using Clinica.Controller;

class Program
{
    static void Main(string[] args)
    {
        IPacienteController paciente = new PacienteController();
        IConsultaController consulta = new ConsultaController();

        Console.Clear();

        while (true)
        {
            Console.Clear();
            Start();
            Console.WriteLine("1 - Cadastrar um paciente");
            Console.WriteLine("2 - Marcações de consultas");
            Console.WriteLine("3 - Cancelamento de consultas");
            Console.WriteLine("4 - Sair");

            Console.Write("\nEscolha uma das opções acima: ");
            string opcao = Console.ReadLine()!;
            Console.Clear();
            switch (opcao)
            {
                case "1":
                    paciente.CadastrarPaciente();
                    break;
                case "2":
                    consulta.MarcarConsulta();
                    break;
                case "3":
                    consulta.CancelarConsulta();
                    break;
                case "4":
                    Exit();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nOpção Invalida!");
                    break;
            }
        }
    }

    private static void Exit()
    {
        Console.WriteLine(@"
▒█▀▀▀█ █▀▀▄ █▀▀█ ░▀░ █▀▀▀ █▀▀█ █▀▀▄ █▀▀█ 　 █▀▀█ █▀▀█ █▀▀█ 　 █░░█ █▀▀ █▀▀█ █▀▀█ 　 █▀▀▄ █▀▀█ █▀▀ █▀▀ █▀▀█ 
▒█░░▒█ █▀▀▄ █▄▄▀ ▀█▀ █░▀█ █▄▄█ █░░█ █░░█ 　 █░░█ █░░█ █▄▄▀ 　 █░░█ ▀▀█ █▄▄█ █▄▄▀ 　 █░░█ █░░█ ▀▀█ ▀▀█ █░░█ 
▒█▄▄▄█ ▀▀▀░ ▀░▀▀ ▀▀▀ ▀▀▀▀ ▀░░▀ ▀▀▀░ ▀▀▀▀ 　 █▀▀▀ ▀▀▀▀ ▀░▀▀ 　 ░▀▀▀ ▀▀▀ ▀░░▀ ▀░▀▀ 　 ▀░░▀ ▀▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀▀ 

█▀▀ ░▀░ █▀▀ ▀▀█▀▀ █▀▀ █▀▄▀█ █▀▀█ █ 
▀▀█ ▀█▀ ▀▀█ ░░█░░ █▀▀ █░▀░█ █▄▄█ ▀ 
▀▀▀ ▀▀▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░░░▀ ▀░░▀ ▄");
    }

    private static void Start()
    {
        Console.WriteLine(@"


▒█▀▀█ █░░ ░▀░ █▀▀▄ ░▀░ █▀▀ █▀▀█ 　 █▀▀▄ █▀▀ 　 ▒█▀▀█ █▀▀█ █▀▀▄ █▀▀ █░░█ █░░ ▀▀█▀▀ █▀▀█ █▀▀ 
▒█░░░ █░░ ▀█▀ █░░█ ▀█▀ █░░ █▄▄█ 　 █░░█ █▀▀ 　 ▒█░░░ █░░█ █░░█ ▀▀█ █░░█ █░░ ░░█░░ █▄▄█ ▀▀█ 
▒█▄▄█ ▀▀▀ ▀▀▀ ▀░░▀ ▀▀▀ ▀▀▀ ▀░░▀ 　 ▀▀▀░ ▀▀▀ 　 ▒█▄▄█ ▀▀▀▀ ▀░░▀ ▀▀▀ ░▀▀▀ ▀▀▀ ░░▀░░ ▀░░▀ ▀▀▀ 

░█▀▀█ █▀▀▀ ░▀░ █░░ 
▒█▄▄█ █░▀█ ▀█▀ █░░ 
▒█░▒█ ▀▀▀▀ ▀▀▀ ▀▀▀");
    }
}