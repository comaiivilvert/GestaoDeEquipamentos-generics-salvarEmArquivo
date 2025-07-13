using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Arquivos.ModuloChamado;
using GestaoDeEquip.Infra.Arquivos.moduloEquipamento;
using GestaoDeEquip.Infra.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
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
            return View(chamados);
        }

        public IActionResult Cadastrar()
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();
            return View(equipamentos);
        }

        [HttpPost]
        public IActionResult Cadastrar(string titulo, string descricao, DateTime dataAbertura, Guid equipamentoId)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(equipamentoId);

            Chamado novoChamado = new Chamado(titulo, descricao, dataAbertura, equipamentoSelecionado);

            repositorioChamado.CadastrarRegistro(novoChamado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(Guid Id)
        {

            Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(Id);

            if (chamadoSelecionado == null)
                return RedirectToAction(nameof(Index));

            return View(chamadoSelecionado);
        }


        [HttpPost]
        public IActionResult Editar(Guid id, string titulo, string descricao, DateTime dataAbertura, Guid equipamentoId)
        {

            Equipamento novoEquipamento = repositorioEquipamento.SelecionarRegistroPorId(equipamentoId);

            Chamado chamadoEditado = new Chamado(titulo, descricao, dataAbertura, novoEquipamento);

            bool edicaoConcluida = repositorioChamado.EditarRegistro(id, chamadoEditado);

            if (!edicaoConcluida)
            {
                chamadoEditado.Id = id;
                return View(chamadoEditado);

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(Id);

            if (chamadoSelecionado == null)
                return RedirectToAction(nameof(Index));

            return View(chamadoSelecionado);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {

            repositorioChamado.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }


    }
}
