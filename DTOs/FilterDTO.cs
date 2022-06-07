using System.ComponentModel.DataAnnotations;

namespace TestREST.DTOs
{
    public record FilterDTO
    {
        [Required]
        [StringLength(2)]
        [RegularExpression("[=<>][=]?")]
        public string Operator { get; init; }

        [Required]
        [RegularExpression("(price|amount|created_date)")]
        public string Attribute { get; init; }

        [Required]
        [Range(0, 1000)]
        public decimal Comparator { get; init; }

        public FilterDTO()
        {
            if (Operator == null)
            {
                Operator = "Missing Operator";
            }
            if (Attribute == null)
            {
                Attribute = "Missing Attribute";
            }
        }
    }
}