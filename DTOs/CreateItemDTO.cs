using System.ComponentModel.DataAnnotations;

namespace TestREST.DTOs
{
    public record CreateItemDTO
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }

        [Required]
        [Range(0,1000)]
        public int Amount { get; init; }

        public CreateItemDTO()
        {
            if (Name==null){
                Name = "Name Missing.";
            }
        }
    }
}