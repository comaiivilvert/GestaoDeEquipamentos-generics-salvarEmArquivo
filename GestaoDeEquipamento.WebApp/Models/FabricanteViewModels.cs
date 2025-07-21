using GestaoDeEquipamentos.Dominio.ModuloFabricante;

public class CadastrarFabricanteViewModel
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public CadastrarFabricanteViewModel()
    {
    }
}

public class EditarFabricanteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public EditarFabricanteViewModel()
    {
    }

    public EditarFabricanteViewModel(Guid id, string nome, string email, string telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}

public class ExcluirFabricanteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirFabricanteViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarFabricantesViewModel
{
    public List<DetalhesFabricanteViewModel> Registros { get; set; }

    public VisualizarFabricantesViewModel(List<Fabricante> fabricantes)
    {
        Registros = new List<DetalhesFabricanteViewModel>();

        foreach (Fabricante f in fabricantes)
        {
            DetalhesFabricanteViewModel detalhesVm = new DetalhesFabricanteViewModel(
                f.Id,
                f.Nome,
                f.Email,
                f.Telefone
            );

            Registros.Add(detalhesVm);
        }
    }
}

public class DetalhesFabricanteViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public DetalhesFabricanteViewModel(Guid id, string nome, string email, string telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}