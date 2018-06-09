using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionMarket;
using Products;
using Serializatior;

namespace Lab16
{
    class Program
    {
        static Serializator serializator = new Serializator();
        private static MyNewCollection collection = new MyNewCollection("Магазин");
        static Journal journal = new Journal();
        static void Main(string[] args)
        {
           collection.CollectionProductChanged += new CollectionHandler(journal.CollectionProductChanged);
           collection.DiscountProductIsEnd+=new Discounter(journal.ProductStorageLifeEnd);
           collection.CollectionProductCountChanged+= new CollectionHandler(journal.CollectionProductCountChanged);
           FoodProduct.DiscountChange+=new Discounter(journal.ProductDiscountChanged);
            collection.Add(new IndustrialProduct("быдых",100,1000,10,DateTime.Now,20));
            Generator.Generator generator = new Generator.Generator();
            List< Product > prod= generator.generate();
            for (int i = 0; i < prod.Count; i++)
            {
                collection.Add(prod[i]);
            }
            while (!Console.CapsLock)
            {


                collection.Print();
                Console.WriteLine("Перейти на следующий день час работы");
                collection.Work();
                Console.ReadKey();
                collection.RemoveOffered();
                serializator.Serialize(collection,$"{collection.Name}___{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}");
            }

            Console.ReadKey();
        }

        public void Menu()
        {
            
            Console.WriteLine("Вас приветствует магазин продуктов");
            Console.WriteLine("1-Выбрать файл с данными");
            Console.WriteLine("2-Посмотреть продукты");
            Console.WriteLine("3-Выбрать магазин");
            Console.WriteLine("4-Выбрать магазин");
            Console.WriteLine("5-Выбрать магазин");

        }
    }
    
}
