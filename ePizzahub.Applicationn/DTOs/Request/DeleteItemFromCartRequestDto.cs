namespace ePizzahub.Applicationn.DTOs.Request
{
    public class DeleteItemFromCartRequestDto
    {
        public Guid CartId { get; set; }
        public int ItemId { get; set; }
    }
}
