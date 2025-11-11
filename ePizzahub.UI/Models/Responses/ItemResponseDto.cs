namespace ePizzahub.UI.Models.Responses
{
    public class ItemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
