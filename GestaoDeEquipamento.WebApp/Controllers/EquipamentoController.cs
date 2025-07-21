using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Arquivos.moduloEquipamento;
using GestaoDeEquip.Infra.Arquivos.ModuloFabricante;
using GestaoDeEquipamento.WebApp.Models;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamento.WebApp.Controllers
{
    public class EquipamentoController : Controller
    {
        private RepositorioEquipamentoEmArquivo repositorioEquipamento;
        private RepositorioFabricanteEmArquivo repositorioFabricante;

        public EquipamentoController()
        {
            ContextoDados contexto = new ContextoDados(true);
            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
            repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
        }

        public IActionResult Index()
        {
            List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();
            VisualizarEquipamentosViewModel visualizarVm = new VisualizarEquipamentosViewModel(equipamentos);

            return View(visualizarVm);
            
        }

        public IActionResult Cadastrar()
        {
            var fabricantes = repositorioFabricante.SelecionarRegistros();

            CadastrarEquipamentoViewModel cadastrarVm = new CadastrarEquipamentoViewModel(fabricantes);
            
            return View(cadastrarVm);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
        {
            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(cadastrarVm.FabricanteId);

            if (fabricanteSelecionado == null)
            {
                return RedirectToAction(nameof(Index));
            }

            Equipamento novoEquipamento = new Equipamento(
                cadastrarVm.Nome, 
                cadastrarVm.PrecoAquisicao, 
                cadastrarVm.NumeroSerie, 
                fabricanteSelecionado, 
                cadastrarVm.DataFabricacao);

            repositorioEquipamento.CadastrarRegistro(novoEquipamento);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(Guid Id)
        {

            var fabricantes = repositorioFabricante.SelecionarRegistros();
            ViewBag.Fabricantes = fabricantes;

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(Id);

            if (equipamentoSelecionado == null)
                return RedirectToAction(nameof(Index));



            return View(equipamentoSelecionado);
        }


        [HttpPost]
        public IActionResult Editar(Guid id, string nome, decimal precoAquisicao, string numeroSerie, Guid fabricanteId, DateTime dataFabricacao)
        {

            Fabricante novoFabricante = repositorioFabricante.SelecionarRegistroPorId(fabricanteId);
            
            Equipamento equipamentoEditado = new Equipamento(nome, precoAquisicao, numeroSerie, novoFabricante,  dataFabricacao);

            bool edicaoConcluida = repositorioEquipamento.EditarRegistro(id, equipamentoEditado);

            if (!edicaoConcluida)
            {
                equipamentoEditado.Id = id;
                return View(equipamentoEditado);

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(Id);

            if (equipamentoSelecionado == null)
                return RedirectToAction(nameof(Index));

            return View(equipamentoSelecionado);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {

            repositorioEquipamento.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }



    }
}
