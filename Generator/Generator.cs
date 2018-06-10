using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                prdProducts.Add(new ConstructionProduct(constructionitems(), rd.Next(100, 15000), "Не указано",
                    "Не указано", rd.Next(1, 50), rd.Next(1, 50), DateTime.Now, 900));
            for (int i = 0; i < count; i++)
                prdProducts.Add(new FoodProduct(foodstr(), rd.Next(100, 2000), DateTime.Now, rd.Next(5, 20)));

            for (int i = 0; i < count; i++)
                prdProducts.Add(new IndustrialProduct(Path.GetRandomFileName(), rd.Next(100, 2000), rd.Next(1, 100),
                    rd.Next(1, 300), DateTime.Now, rd.Next(40)));

            return prdProducts;
        }

        public static string constructionitems()
        {
            string[] temp = new string[]
            {
                "Строительные смеси",
               "Утеплители",
                "Изоляционные материалы",
                "Лакокрасочные материалы",
               "Строительная Химия" ,
                "Клеи",
                "Обои",
                "Сайдинг",
                "Фанера",
                "Двери"
              
            };
            return temp[rd.Next(temp.Length)];
        }

        public static string foodstr()
        {
            string[] temp = new string[]
            {
               "Фрикадельки           "   ,
               "Котлеты              "   ,
               "Суповой набор куриный                           "   ,
               "Суповой набор мясной                             "  ,
               "Готовый мясной бульон                           "   ,
               "Готовый куриный бульон                          "   ,
               "Курица                   "   ,
               "Куриное филе                                      "   ,
               "Свинина порционная по 300 г                     "   ,
               "Сало                                               "   ,
               "Стручковая фасоль                                "   ,
               "Маргарин                                           "  ,
               "Сливочное масло                                  "   ,
               "Ягоды   "   ,
               "Отварные грибы                                    "  ,
               "Шпинат                                             "   ,
               "Слоеное тесто                                     "   ,
               "Рыба                                     "   ,
               "Крабовые палочки                                 "   ,

            };
            return temp[rd.Next(temp.Length)].Trim();
        }

        public static string factory()
        {
            string[] temp = new string[]
            {
                "Строительные смеси",
                "Утеплители",
                "Изоляционные материалы",
                "Лакокрасочные материалы",
                "Строительная Химия" ,
                "Клеи",
                "Обои",
                "Сайдинг",
                "Фанера",
                "Двери"

            };
            return temp[rd.Next(temp.Length)];
        }

    }
}
