using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Arquivos.ModuloChamado;
using GestaoDeEquip.Infra.Arquivos.moduloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamento.WebApp.Controllers
{
    public class ChamadoController : Controller
    {

        private RepositorioEquipamentoEmArquivo repositorioEquipamento;
        private RepositorioChamadoEmArquivo repositorioChamado;

        public ChamadoController()
        {
            ContextoDados contexto = new ContextoDados(true);
            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
            repositorioChamado = new RepositorioChamadoEmArquivo(contexto);
        }
        public IActionResult Index()
        {
            List<Chamado> chamados = repositorioChamado.SelecionarRegistros();

            VisualizarChamadosViewModel visualizarVm = new VisualizarChamadosViewModel(chamados);

            return View(visualizarVm);
        }

        public IActionResult Cadastrar()
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();
            
            CadastrarChamadoViewModel cadastrarVm = new CadastrarChamadoViewModel(equipamentos);

            return View(cadastrarVm);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(cadastrarVm.EquipamentoId);

            Chamado novoChamado = new Chamado(cadastrarVm.Titulo, cadastrarVm.Descricao, cadastrarVm.DataAbertura, equipamentoSelecionado);

            repositorioChamado.CadastrarRegistro(novoChamado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(Guid Id)
        {

            Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(Id);

            List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

            EditarChamadoViewModel editarVm = new EditarChamadoViewModel(
                chamadoSelecionado.Id,
                chamadoSelecionado.Titulo,
                chamadoSelecionado.Descricao,
                chamadoSelecionado.DataAbertura,
                chamadoSelecionado.Equipamento.Id,
                equipamentos
                );

            return View(editarVm);
        }


        [HttpPost]
        public IActionResult Editar(Guid Id,EditarChamadoViewModel editarVm)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(editarVm.EquipamentoId);

            Chamado chamadoEditado = new Chamado(editarVm.Titulo, editarVm.Descricao, editarVm.DataAbertura, equipamentoSelecionado);

            repositorioChamado.EditarRegistro(Id, chamadoEditado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(Id);

            ExcluirChamadoViewModel excluirVm = new ExcluirChamadoViewModel(chamadoSelecionado.Id, chamadoSelecionado.Titulo);

            return View(excluirVm);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {

            repositorioChamado.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }


    }
}
