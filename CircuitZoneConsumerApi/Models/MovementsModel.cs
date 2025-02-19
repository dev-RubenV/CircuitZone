namespace CircuitZoneConsumerApi.Models
{
    public class MovementsModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public int TipoMovimentoId { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
    }
}
