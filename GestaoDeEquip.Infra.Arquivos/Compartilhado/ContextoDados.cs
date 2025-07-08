using GestaoDeEquipamentos.Dominio.ModuloChamado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using System.Runtime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestaoDeEquip.Infra.Arquivos.Compartilhado
{
    public class ContextoDados
    {
        public List<Fabricante> Fabricantes { get; set; }
        public List<Equipamento> Equipamentos { get; set; }
        public List<Chamado> Chamados { get; set; }

        private string pastaArmazenamento = "C:\\temp";
        private string arquivosArmazenamento = "dados.json";

        public ContextoDados(bool carregarDados) : this()
        {
            if (carregarDados)
                Carregar();
        }

        public ContextoDados() 
        {
            Fabricantes = new List<Fabricante>();
            Equipamentos = new List<Equipamento>();
            Chamados = new List<Chamado>();
        }

        public void Salvar()
        {
            string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivosArmazenamento);

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;
            jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

            string conteudoJson = JsonSerializer.Serialize(this, jsonOptions);

            Directory.CreateDirectory(pastaArmazenamento);

            File.WriteAllText(caminhoCompleto, conteudoJson);

        }

        public void Carregar()
        {
            string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivosArmazenamento);

            if (!File.Exists(caminhoCompleto))
                return;

            string conteudoJson = File.ReadAllText(caminhoCompleto);

            if (string.IsNullOrEmpty(caminhoCompleto))
                return;

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

            ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(conteudoJson, jsonOptions)!;

            if (contextoArmazenado == null) 
                return;
            
            this.Fabricantes = contextoArmazenado.Fabricantes;
            this.Equipamentos = contextoArmazenado.Equipamentos;
            this.Chamados = contextoArmazenado.Chamados;

        }

    }
}
