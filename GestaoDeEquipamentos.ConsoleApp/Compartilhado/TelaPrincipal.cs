using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioFabricante repositorioFabricante;
    private RepositorioEquipamento repositorioEquipamento;
    private RepositorioChamado repositorioChamado;

    private TelaFabricante telaFabricante;
    private TelaEquipamento telaEquipamento;
    private TelaChamado telaChamado;

    public TelaPrincipal()
    {
        repositorioFabricante = new RepositorioFabricante();
        repositorioEquipamento = new RepositorioEquipamento();
        repositorioChamado = new RepositorioChamado();

        telaFabricante = new TelaFabricante(repositorioFabricante);

        telaEquipamento = new TelaEquipamento(
            repositorioEquipamento,
            repositorioFabricante
        );

        telaChamado = new TelaChamado(repositorioChamado, repositorioEquipamento);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|        Gestão de Equipamentos        |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Fabricantes");
        Console.WriteLine("2 - Controle de Equipamentos");
        Console.WriteLine("3 - Controle de Chamados");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoEscolhida = Console.ReadLine()[0];
    }

    public ITela? ObterTela()
    {
        if (opcaoEscolhida == '1')
            return telaFabricante;

        else if (opcaoEscolhida == '2')
            return telaEquipamento;

        else if (opcaoEscolhida == '3')
            return telaChamado;

        return null;
    }
}
