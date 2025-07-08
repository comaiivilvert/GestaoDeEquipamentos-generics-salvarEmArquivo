using GestaoDeEquip.Infra.Arquivos.Compartilhado;
using GestaoDeEquip.Infra.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;


namespace GestaoDeEquip.Infra.Arquivos.moduloEquipamento

{
    public class RepositorioEquipamentoEmArquivo : RepositorioBaseEmArquivo<Equipamento>
    {
        public RepositorioEquipamentoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Equipamento> ObterRegistros()
        {
            return contexto.Equipamentos;
        }
    }
}
