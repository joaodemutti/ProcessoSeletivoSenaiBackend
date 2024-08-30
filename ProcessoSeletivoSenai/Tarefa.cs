namespace ProcessoSeletivoSenai
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public Status Status { get; set; }

    }

    public enum Status
    {
        Pendente,
        Concluida
    }
}
