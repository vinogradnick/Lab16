using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Products;

namespace Generator
{
    class Generator
    {
        private VirtualTimer time;
        private Product[] product;
        private FoodProduct[] food;
        private IndustrialProduct[] insIndustrialProducts;
        private ConstructionProduct constructionProduct;

        public Generator()
        {
            int count = DateTime.Now.Second;
            product = new Product[count*4];

        }
    }
}
