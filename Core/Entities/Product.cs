using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        [ForeignKey("ProductTypeId")]
        public int ProductTypeId { get; set; }
        public ProductTypes ProductType { get; set; }

        [ForeignKey("ProductBrandsId")]
        public int ProductBrandId { get; set; }
        public ProductBrands ProductBrand { get; set; }
    }
}
