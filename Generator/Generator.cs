using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Products;

namespace Generator
{
    public class Generator
    {
        private VirtualTimer time;
        private FoodProduct[] food;
        private IndustrialProduct[] insIndustrialProducts;
        private ConstructionProduct[] constructionProduct;
        public static int count = 5;
        static Faker bogus = new Bogus.Faker("ru");
        public Generator()
        {
            
            food = new FoodProduct[count];
            insIndustrialProducts=new IndustrialProduct[count];
            constructionProduct = new ConstructionProduct[count];
        }
        static Random rd = new Random();

        public  static List<Product> generate()
        {
            
            
            List<Product> prdProducts=new List<Product>();
            for (int i = 0; i < count; i++)
                prdProducts.Add(new ConstructionProduct(bogus.Commerce.ProductName(), rd.Next(100, 15000), bogus.Commerce.ProductMaterial(),
                    bogus.Commerce.ProductMaterial(), rd.Next(1, 50), rd.Next(1, 50), DateTime.Now, 900));
            for (int i = 0; i < count; i++)
                prdProducts.Add(new FoodProduct(bogus.Commerce.ProductName(), rd.Next(100, 2000), DateTime.Now, rd.Next(5, 20)));

            for (int i = 0; i < count; i++)
                prdProducts.Add(new IndustrialProduct(bogus.Commerce.ProductName(), rd.Next(100, 2000), rd.Next(1, 100),
                    rd.Next(1, 300), DateTime.Now, rd.Next(40)));

            return prdProducts;
        }

        public static string gen() => bogus.Company.CompanyName();
    }
}
