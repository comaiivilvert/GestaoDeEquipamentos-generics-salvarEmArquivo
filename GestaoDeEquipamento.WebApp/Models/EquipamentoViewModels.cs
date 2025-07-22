using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;

namespace GestaoDeEquipamento.WebApp.Models
{
    public class CadastrarEquipamentoViewModel
    {
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public string NumeroSerie { get; set; }
        public Guid FabricanteId { get; set; }
        public List<SelecionarFabricanteViewModel> FabricantesDisponiveis { get; set; }
        public DateTime DataFabricacao { get; set; }

        public CadastrarEquipamentoViewModel()
        {
            FabricantesDisponiveis = new List<SelecionarFabricanteViewModel>();
        }

        public CadastrarEquipamentoViewModel(List<Fabricante> fabricantes) : this()
        {

            foreach (Fabricante f in fabricantes)
            {
                SelecionarFabricanteViewModel selecionarVm =
                    new SelecionarFabricanteViewModel(f.Id, f.Nome);

                FabricantesDisponiveis.Add(selecionarVm);
            }
        }
    }

    public class EditarEquipamentoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public string NumeroSerie { get; set; }
        public Guid FabricanteId { get; set; }
        public List<SelecionarFabricanteViewModel> FabricantesDisponiveis { get; set; }
        public DateTime DataFabricacao { get; set; }

        public EditarEquipamentoViewModel()
        {
            FabricantesDisponiveis = new List<SelecionarFabricanteViewModel>();
        }

        public EditarEquipamentoViewModel(
            Guid id,
            string nome,
            decimal precoAquisicao,
            string numeroSerie,
            Guid fabricanteId,
            List<Fabricante> fabricantes,
            DateTime dataFabricacao
        ) : this()
        {

            foreach (Fabricante f in fabricantes)
            {
                SelecionarFabricanteViewModel selecionarVm =
                    new SelecionarFabricanteViewModel(f.Id, f.Nome);

                FabricantesDisponiveis.Add(selecionarVm);
            }

            Id = id;
            Nome = nome;
            PrecoAquisicao = precoAquisicao;
            NumeroSerie = numeroSerie;
            FabricanteId = fabricanteId;
            DataFabricacao = dataFabricacao;
        }
    }

    public class ExcluirEquipamentoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ExcluirEquipamentoViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class SelecionarFabricanteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelecionarFabricanteViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VisualizarEquipamentosViewModel
    {
        public List<DetalhesEquipamentoViewModel> Registros { get; set; }

        public VisualizarEquipamentosViewModel(List<Equipamento> equipamentos)
        {
            Registros = new List<DetalhesEquipamentoViewModel>();

            foreach (Equipamento e in equipamentos)
            {
                DetalhesEquipamentoViewModel detalhesVM = new DetalhesEquipamentoViewModel(
                    e.Id,
                    e.Nome,
                    e.PrecoAquisicao,
                    e.NumeroSerie,
                    e.Fabricante.Nome,
                    e.DataFabricacao
                );

                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesEquipamentoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public string NumeroSerie { get; set; }
        public string NomeFabricante { get; set; }
        public DateTime DataFabricacao { get; set; }

        public DetalhesEquipamentoViewModel(
            Guid id,
            string nome,
            decimal precoAquisicao,
            string numeroSerie,
            string nomeFabricante,
            DateTime dataFabricacao
        )
        {
            Id = id;
            Nome = nome;
            PrecoAquisicao = precoAquisicao;
            NumeroSerie =  numeroSerie;
            NomeFabricante = nomeFabricante;
            DataFabricacao = dataFabricacao;
        }

        public override string ToString()
        {
            return $"Id: {Id} - Nome: {Nome} - Fabricante: {NomeFabricante} - Preço de Aquisição: {PrecoAquisicao:C2} - Data de Fabricação: {DataFabricacao:d}";
        }
    }
}
