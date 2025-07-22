
using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;

namespace GestaoDeEquipamentos.WebApp.Models;

public class CadastrarChamadoViewModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public Guid EquipamentoId { get; set; }
    public List<SelecionarEquipamentoViewModel> EquipamentosDisponiveis { get; set; }

    public CadastrarChamadoViewModel()
    {
        EquipamentosDisponiveis = new List<SelecionarEquipamentoViewModel>();
    }

    public CadastrarChamadoViewModel(List<Equipamento> equipamentos) : this()
    {
        foreach (Equipamento e in equipamentos)
        {
            SelecionarEquipamentoViewModel selecionarVm =
                new SelecionarEquipamentoViewModel(e.Id, e.Nome);

            EquipamentosDisponiveis.Add(selecionarVm);
        }
    }
}

public class EditarChamadoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public Guid EquipamentoId { get; set; }

    public List<SelecionarEquipamentoViewModel> EquipamentosDisponiveis { get; set; }

    public EditarChamadoViewModel()
    {
        EquipamentosDisponiveis = new List<SelecionarEquipamentoViewModel>();
    }

    public EditarChamadoViewModel(Guid id, string titulo, string descricao, DateTime dataAbertura, Guid equipamentoId, List<Equipamento> equipamentos) : this()
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        EquipamentoId = equipamentoId;

        foreach (Equipamento e in equipamentos)
        {
            SelecionarEquipamentoViewModel selecionarVm =
                new SelecionarEquipamentoViewModel(e.Id, e.Nome);

            EquipamentosDisponiveis.Add(selecionarVm);
        }
    }
}

public class ExcluirChamadoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirChamadoViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}

public class SelecionarEquipamentoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public SelecionarEquipamentoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}


public class VisualizarChamadosViewModel
{
    public List<DetalhesChamadoViewModel> Registros { get; set; }

    public VisualizarChamadosViewModel(List<Chamado> chamados)
    {
        Registros = new List<DetalhesChamadoViewModel>();

        foreach (Chamado c in chamados)
        {
            DetalhesChamadoViewModel detalhesVM = new DetalhesChamadoViewModel(
                c.Id,
                c.Titulo,
                c.Descricao,
                c.DataAbertura,
                c.Equipamento.Nome
            );

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesChamadoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public string NomeEquipamento { get; set; }

    public DetalhesChamadoViewModel(Guid id, string titulo, string descricao, DateTime dataAbertura, string nomeEquipamento)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        NomeEquipamento = nomeEquipamento;
    }
}
