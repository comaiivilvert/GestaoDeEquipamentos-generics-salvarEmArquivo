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

            return RedirectToAction("Index");
        }
    }
}
