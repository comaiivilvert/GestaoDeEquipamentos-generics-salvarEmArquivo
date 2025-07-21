using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using Microsoft.AspNetCore.Identity.UI.Services;
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

            VisualizarFabricantesViewModel visualizarVm = new VisualizarFabricantesViewModel(fabricantes);

            return View(visualizarVm);
        }

        public IActionResult Cadastrar()
        {
            CadastrarFabricanteViewModel cadastrarVm = new CadastrarFabricanteViewModel();

            return View(cadastrarVm);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVm)
        {
            Fabricante novoFabricante = new Fabricante(cadastrarVm.Nome, cadastrarVm.Email, cadastrarVm.Telefone);

            repositorioFabricante.CadastrarRegistro(novoFabricante);

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Editar(Guid Id)
        {
            
            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(Id);

            if (fabricanteSelecionado == null)
                return RedirectToAction(nameof(Index));

            EditarFabricanteViewModel editarVm = new EditarFabricanteViewModel(
                fabricanteSelecionado.Id,
                fabricanteSelecionado.Nome,
                fabricanteSelecionado.Email,
                fabricanteSelecionado.Telefone
                );

            return View(editarVm);
        }


        [HttpPost]
        public IActionResult Editar(Guid Id, EditarFabricanteViewModel editarVm)
        {
            Fabricante fabricanteEditado = new Fabricante(editarVm.Nome, editarVm.Email, editarVm.Telefone);

            bool edicaoConcluida = repositorioFabricante.EditarRegistro(Id, fabricanteEditado);

            if (!edicaoConcluida)
            {
                fabricanteEditado.Id = Id;
                return View(fabricanteEditado);

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(Guid Id)
        {

            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(Id);

            if (fabricanteSelecionado == null)
                return RedirectToAction(nameof(Index));

            ExcluirFabricanteViewModel excluiVm = new ExcluirFabricanteViewModel(Id, fabricanteSelecionado.Nome);


            return View(excluiVm);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(Guid Id)
        {
            
            repositorioFabricante.ExcluirRegistro(Id);

            return RedirectToAction(nameof(Index));

        }



    }
}
