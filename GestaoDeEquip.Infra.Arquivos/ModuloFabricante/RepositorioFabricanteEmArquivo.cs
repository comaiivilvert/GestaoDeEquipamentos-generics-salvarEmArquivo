using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;

namespace GestaoDeEquip.Infra.Arquivos.ModuloFabricante
{
    public class RepositorioFabricanteEmArquivo : RepositorioBaseEmArquivo<Fabricante>
    {
        public RepositorioFabricanteEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Fabricante> ObterRegistros()
        {
            return contexto.Fabricantes;
        }
    }
}
