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
            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(Id);

            var fabricantes = repositorioFabricante.SelecionarRegistros();

            EditarEquipamentoViewModel editarVm = new EditarEquipamentoViewModel(
                equipamentoSelecionado.Id,
                equipamentoSelecionado.Nome,
                equipamentoSelecionado.PrecoAquisicao,
                equipamentoSelecionado.NumeroSerie,
                equipamentoSelecionado.Fabricante.Id,
                fabricantes,
                equipamentoSelecionado.DataFabricacao
                );


            if (equipamentoSelecionado == null)
                return RedirectToAction(nameof(Index));

            return View(editarVm);
        }


        [HttpPost]
        public IActionResult Editar(Guid Id, EditarEquipamentoViewModel editarVm)
        {

            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(editarVm.FabricanteId);
            
            Equipamento equipamentoEditado = new Equipamento
                
                (editarVm.Nome, 
                editarVm.PrecoAquisicao, 
                editarVm.NumeroSerie,
                fabricanteSelecionado,
                editarVm.DataFabricacao);

                repositorioEquipamento.EditarRegistro(Id, equipamentoEditado);

                return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(Id);

            ExcluirEquipamentoViewModel excluirVm = new ExcluirEquipamentoViewModel(
                equipamentoSelecionado.Id,
                equipamentoSelecionado.Nome
                );

            return View(excluirVm);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {

            repositorioEquipamento.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }



    }
}
