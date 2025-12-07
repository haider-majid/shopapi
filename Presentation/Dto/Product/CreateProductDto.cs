using Presentation.Dto.Category;
using Presentation.Dto.Product;namespace Presentation.Dto.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        //foreign key
        public Guid CategoryId { get; set; }
    }
}
