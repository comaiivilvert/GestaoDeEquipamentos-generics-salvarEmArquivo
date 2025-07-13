using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GestaoDeEquipamento.WebApp.Controllers
{
    public class FabricanteController : Controller
    {

        private RepositorioFabricanteEmArquivo repositorioFabricante;

        public FabricanteController()
        {
            ContextoDados contexto = new ContextoDados(true);
            repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
        }
        public IActionResult Index()
        {
            List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();
            return View(fabricantes);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string telefone)
        {
            Fabricante novoFabricante = new Fabricante(nome, email, telefone);

            repositorioFabricante.CadastrarRegistro(novoFabricante);

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Editar(Guid Id)
        {
            
            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(Id);

            if (fabricanteSelecionado == null)
                return RedirectToAction(nameof(Index));



            return View(fabricanteSelecionado);
        }


        [HttpPost]
        public IActionResult Editar(Guid id, string nome, string email, string telefone)
        {
            Fabricante fabricanteEditado = new Fabricante(nome, email, telefone);

            bool edicaoConcluida = repositorioFabricante.EditarRegistro(id, fabricanteEditado);

            if (!edicaoConcluida)
            {
                fabricanteEditado.Id = id;
                return View(fabricanteEditado);

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(Id);

            if (fabricanteSelecionado == null)
                return RedirectToAction(nameof(Index));



            return View(fabricanteSelecionado);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {
            
            repositorioFabricante.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }



    }
}
