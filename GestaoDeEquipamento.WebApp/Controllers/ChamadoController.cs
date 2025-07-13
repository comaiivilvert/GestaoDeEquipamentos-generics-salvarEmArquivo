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
        public IActionResult Cadastrar(string titulo, string descricao, DateTime dataAbertura, int equipamentoId)
        {

            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(equipamentoId);

            Chamado novoChamado = new Chamado(titulo, descricao, dataAbertura, equipamentoSelecionado);

            repositorioChamado.CadastrarRegistro(novoChamado);

            return RedirectToAction(nameof(Index));
        }


    }
}
