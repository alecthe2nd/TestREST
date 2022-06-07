using TestREST.Entities;
using TestREST.DTOs;

namespace TestREST
{
    public static class Extensions
    {
        public static ItemDTO asDTO( this Item item)
        {
            var dto = new ItemDTO(){
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Amount = item.Amount,
                CreatedDate = item.CreatedDate,
            };
            return dto;
        }
    }
}