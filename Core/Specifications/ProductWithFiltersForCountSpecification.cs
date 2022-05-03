using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams ProductParams)
        : base(x =>
              (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower().Contains(ProductParams.Search))
           && (!ProductParams.TypeId.HasValue || x.ProductTypeId == ProductParams.TypeId) 
           && (!ProductParams.BrandId.HasValue || x.ProductBrandId == ProductParams.BrandId))
        {

        }
    }
}
