using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypesandBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesandBrandsSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        public ProductsWithTypesandBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

    }
}
