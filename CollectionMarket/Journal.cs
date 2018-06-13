using System;
using System.Collections.Generic;
using System.Globalization;
using Products;

namespace CollectionMarket
{
    [Serializable]
    public class Journal
    {
        public List<JournalEntry> JournalList;
        public List<JournalEntryProduct> JournalEntryProducts;
        public List<JournalEntryProduct> DeadProducts;

        public Journal()
        {
            JournalList = new List<JournalEntry>();
            JournalEntryProducts=new List<JournalEntryProduct>();
            DeadProducts = new List<JournalEntryProduct>();
        }

        public void CollectionProductChanged(object source, CollectionHandlerArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.TypeChange, e.product);
            JournalList.Add(je);
        }

        public void CollectionProductCountChanged(object source, CollectionHandlerArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.TypeChange, e.product);
            JournalList.Add(je);
        }
        public void ProductDiscountChanged(object source, DiscountHandler e)
        {
            JournalEntryProduct je = new JournalEntryProduct(e.Message,e.Price,e.Discount,e.StorageLife,e.Product);
            JournalEntryProducts.Add(je);
        }

        public void ProductStorageLifeEnd(object source, DiscountHandler e)
        {
            if (e.StorageLife == 0)
            {
                JournalEntryProduct je = new JournalEntryProduct(e.Message, e.Price, e.Discount, e.StorageLife, e.Product);
                DeadProducts.Add(je);
            }

        }



        [Serializable]

        /// <summary>
        /// Элементы коллекции журнала
        /// </summary>
        public class JournalEntry
        {
            /// <summary>
            /// Название коллекции
            /// </summary>
            public string Name;

            /// <summary>
            /// Тип собития в коллекции
            /// </summary>
            public string TypeChange;

            /// <summary>
            /// Объект коллекции от которого пришло событие
            /// </summary>
            public object ObjectColelction;

            public JournalEntry(string name, string change, object obj)
            {
                Name = name;
                TypeChange = change;
                ObjectColelction = obj;
            }

            /// <summary>
            /// Вывод информации
            /// </summary>
            /// <returns></returns>
            public override string ToString() =>
                $"Название:{this.Name}\nТип изменения:{this.TypeChange}\nОбъект:{this.ObjectColelction}\n";
        }

        public void Print()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Журнал событий");
            Console.WriteLine("------------------------------------------");
            foreach (var item in this.JournalList)
                Console.WriteLine(item.ToString());
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Список списанных продуктов");
            if(JournalEntryProducts.Count==0) Console.WriteLine("Пуст");
            foreach (var item in JournalEntryProducts)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("------------------------------------------");

        }
        [Serializable]

        public class JournalEntryProduct
        {
            public string Message { get; set; }
            public double Price { get; set; }
            public double Discount;
            public int StorageLife { get; set; }
            public Object Product;

            public JournalEntryProduct(string message, double price, double discount,int life, object product)
            {
                Message = message;
                Price = price;
                StorageLife = life;
                Discount = discount;
                this.Product = product;
            }

            public override string ToString() => $"Тип изменения:{this.Message}\nСрок годности:{StorageLife}\nОбъект:{this.Product.ToString()}";
        }

    }
}