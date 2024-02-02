namespace productApi.Application.Commands.Products
{
    public class UpdateProductCommand
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
    }
}
