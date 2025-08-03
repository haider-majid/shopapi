using Presentation.Dto.Category;
using Presentation.Dto.Product;

namespace Presentation.Dto.Category
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}