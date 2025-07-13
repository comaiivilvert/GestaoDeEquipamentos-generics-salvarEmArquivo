using GestaoDeEquipamentos.Dominio.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;

namespace GestaoDeEquipamentos.Dominio.ModuloChamado;

public class Chamado : EntidadeBase<Chamado>
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public Equipamento Equipamento { get; set; }

    public Chamado()
    {

    }

    public Chamado(
        string titulo,
        string descricao,
        DateTime dataAbertura,
        Equipamento equipamento
    ) : this()
    {
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        Equipamento = equipamento;
    }

    public override void AtualizarRegistro(Chamado registroAtualizado)
    {
        Titulo = registroAtualizado.Titulo;
        Descricao = registroAtualizado.Descricao;
        DataAbertura = registroAtualizado.DataAbertura;
        Equipamento = registroAtualizado.Equipamento;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Titulo))
            erros += "O campo \"Título\" é obrigatório.\n";

        else if (Titulo.Length < 3)
            erros += "O campo \"Título\" precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Descricao))
            erros += "O campo \"Descrição\" é obrigatório.\n";

        if (Equipamento == null)
            erros += "O campo \"Equipamento\" é obrigatório.\n";

        return erros;
    }
}
