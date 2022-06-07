namespace TestREST.DTOs
{
    public class ItemDTO
    {
        public Guid Id {get; init;}
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int Amount { get; init; }
        public DateTimeOffset CreatedDate { get; set; }

        public ItemDTO()
        {
            if (Name==null){
                Name = "Name Missing.";
            }
        }
    }
}