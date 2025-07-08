using GestaoDeEquipamentos.Dominio.Compartilhado;
using System.Net.Mail;

namespace GestaoDeEquipamentos.Dominio.ModuloFabricante;

public class Fabricante : EntidadeBase<Fabricante>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public Fabricante(string nome, string email, string telefone)
    {
        this.Nome = nome;
        this.Email = email;
        this.Telefone = telefone;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O nome é obrigatório!\n";

        else if (Nome.Length < 2)
            erros += "O nome deve conter mais que 1 caractere!\n";

        if (!MailAddress.TryCreate(Email, out _))
            erros += "O email deve conter um formato válido \"nome@provedor.com\"!\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O telefone é obrigatório!\n";

        else if (Telefone.Length < 9)
            erros += "O telefone deve conter no mínimo 9 caracteres!\n";

        return erros;
    }

    public override void AtualizarRegistro(Fabricante registroAtualizado)
    {
        this.Nome = registroAtualizado.Nome;
        this.Email = registroAtualizado.Email;
        this.Telefone = registroAtualizado.Telefone;
    }
}
