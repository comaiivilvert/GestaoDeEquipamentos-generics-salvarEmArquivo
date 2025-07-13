using GestaoDeEquip.Infra.Arquivos.ModuloChamado;
using GestaoDeEquip.Infra.Arquivos.moduloEquipamento;
using GestaoDeEquip.Infra.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

public class TelaChamado : TelaBase<Chamado>, ITela
{
    private RepositorioChamadoEmArquivo repositorioChamado;
    private RepositorioEquipamentoEmArquivo repositorioEquipamento;

    public TelaChamado(
        RepositorioChamadoEmArquivo repositorioChamado,
        RepositorioEquipamentoEmArquivo repositorioEquipamento
    ) : base("Chamados", repositorioChamado)
    {
        this.repositorioChamado = repositorioChamado;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Chamados");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -20}",
            "Id", "Título", "Descrição", "Data de Abertura", "Equipamento"
        );

        List<Chamado> chamados = repositorioChamado.SelecionarRegistros();

        foreach (Chamado c in chamados)
        {
            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -20}",
                c.Id, c.Titulo, c.Descricao, c.DataAbertura.ToShortDateString(), c.Equipamento.Nome
            );
        }

        Console.ReadLine();
    }

    protected override Chamado ObterDados()
    {
        Console.Write("Digite o título do chamado: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite a descrição do chamado: ");
        string descricao = Console.ReadLine();

        DateTime dataAbertura = DateTime.Now;

        VisualizarEquipamentos();

        Console.Write("Digite o ID do equipamento que deseja selecionar: ");
        Guid idEquipamento = Guid.Parse(Console.ReadLine());

        Equipamento equipamentoSelecionado =
            repositorioEquipamento.SelecionarRegistroPorId(idEquipamento);

        Chamado chamado = new Chamado(titulo, descricao, dataAbertura, equipamentoSelecionado);

        return chamado;
    }

    private void VisualizarEquipamentos()
    {
        Console.WriteLine();

        Console.WriteLine("Visualização de Equipamentos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -20} | {5, -15}",
            "Id", "Nome", "Preço Aquisição", "Número Série", "Fabricante", "Data Fabricação"
        );

        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

        foreach (Equipamento e in equipamentos)
        {
            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -15} | {4, -20} | {5, -15}",
                e.Id, e.Nome, e.PrecoAquisicao.ToString("C2"), e.NumeroSerie, e.Fabricante.Nome, e.DataFabricacao.ToShortDateString()
            );
        }

        Console.ReadLine();
    }
}
