﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionMarket;
using Products;
using Serializatior;
using Generator;


namespace Lab16
{
    class Program
    {
        static Serializator serializator = new Serializator();
        static Journal journal = new Journal();
       static List<MyNewCollection> list = new List<MyNewCollection>();
        static void Main(string[] args)
        {
            Markerts();
            foreach (var item in list)
                includeDependences(item);

            while (!Console.CapsLock)
            {
                foreach (MyNewCollection item in list)
                    item.Print();
               
                foreach (MyNewCollection item in list)
                    item.Work();
                Console.WriteLine("Перейти на следующий день час работы");
                Console.ReadKey();
                foreach (MyNewCollection item in list)
                {
                    item.RemoveOffered();
                    serializator.Serialize(item, $"{item.Name}___{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}", journal);
                }
            }
            Console.WriteLine("День работы был завершнен");
            
            Console.ReadKey();
        }

        public static void Markerts()
        {

            for (int i = 0; i < 6; i++)
                list.Add(new MyNewCollection(Generator.Generator.gen()));

        }
        static void includeDependences(MyNewCollection collection)
        {
            collection.CollectionProductChanged += new CollectionHandler(journal.CollectionProductChanged);
            collection.DiscountProductIsEnd += new Discounter(journal.ProductStorageLifeEnd);
            collection.CollectionProductCountChanged += new CollectionHandler(journal.CollectionProductCountChanged);
            FoodProduct.DiscountChange += new Discounter(journal.ProductDiscountChanged);
            collection.Add(new IndustrialProduct("быдых", 100, 1000, 10, DateTime.Now, 20));
           fillCollection(collection);
        }

        static void fillCollection(MyNewCollection coll)
        {
            List<Product> prod = Generator.Generator.generate();
            for (int i = 0; i < prod.Count; i++)
            {
                coll.Add(prod[i]);
            }
        }
        public void Menu()
        {
            
            Console.WriteLine("Вас приветствует магазин продуктов");
            Console.WriteLine("1-Выбрать файл с данными за определенные дни");
            Console.WriteLine("2-Посмотреть продукты");
            Console.WriteLine("3-Выбрать магазин");
            Console.WriteLine("4-Выбрать магазин");
            Console.WriteLine("5-Выбрать магазин");

        }
    }
    
}
