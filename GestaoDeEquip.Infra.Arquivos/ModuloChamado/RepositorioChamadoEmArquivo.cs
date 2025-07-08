using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloChamado;


namespace GestaoDeEquip.Infra.Arquivos.ModuloChamado

{
    public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>
    {
        public RepositorioChamadoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Chamado> ObterRegistros()
        {
            return contexto.Chamados;
        }
    }
}
