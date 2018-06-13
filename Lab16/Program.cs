using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CollectionMarket;
using Products;
using Serializatior;
using Generator;
using Validator;


namespace Lab16
{
    class Program
    {
        static Serializator serializator = new Serializator();
        static Journal journal = new Journal();
        static List<MyNewCollection> list = new List<MyNewCollection>();
        static void Main(string[] args)
        {
            Startmenu();
            GetMarket(Select());
        }

        public static MyNewCollection Select()
        {
            try
            {
                int index = InputValidator.InputFromTO(0,list.Count);

                return list[index];
            }
            catch (Exception e)
            {
                Console.WriteLine("Данный магазин нельзя получить");
                
            }

            return Select();
        }

        public static Product Change(Product product)
        {
            Console.WriteLine("Введите название продукта ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите цену продукта");
            int price = InputValidator.InputPositive();
            product.ChangeProduct(name,price);
            return product;
        }

        /// <summary>
        /// Получить магазин
        /// </summary>
        /// <param name="collection"></param>
        private static void GetMarket(MyNewCollection collection)
        {
           
            CollectionAction(collection);
        }
        /// <summary>
        /// Работа с магазином
        /// </summary>
        /// <param name="collection"></param>
        private static void CollectionAction(MyNewCollection collection)
        {
            Console.WriteLine("Выбран магазин "+collection.Name);
            collection.Print();
            Console.WriteLine("1-Работа с продуктами");
            Console.WriteLine("2-Очистить магазин");
            Console.WriteLine("3-Добавить продукты в магазин");
            switch (Console.ReadLine())
            {
                case "1":
                    Actions(collection);
                    break;
                case "2":
                    
                    collection.Clear();
                    Console.WriteLine("Коллекция была очищена");
                    CollectionAction(collection);
                    break;
                case "3":
                    FillCollection(collection);
                    break;
                case "4":
                    Startmenu();
                    break;
                default:
                    Console.WriteLine("Вы выбрали неверный ответ");
                    CollectionAction(collection);
                    break;
            }
            CollectionAction(collection);

        }

        /// <summary>
        /// Действие над товаром
        /// </summary>
        /// <param name="collection"></param>
        public static void Actions(MyNewCollection collection)
        {
            collection.Print();
            Console.WriteLine("1- Печать товара");
            Console.WriteLine("2- Сортировка товара по цене");
            Console.WriteLine("3- Удалить товар");
            Console.WriteLine("4 -Изменить товар");

            switch (Console.ReadLine())
            {
                case "1":
                    
                    Actions(collection);
                    break;
                case "2":
                    collection.SortByPrice();
                    collection.Print();
                    Actions(collection);
                    break;
                case "3":
                    try
                    {
                        Console.WriteLine("Введите индекс элемента");
                        int index = InputValidator.InputFromTO(0, collection.Count);
                        Product product = collection[index];
                        collection.Remove(product);
                        Actions(collection);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Товар нельзя удалить");
                    }
                    break;
                case "4":
                    try
                    {
                     Console.WriteLine("Введите индекс элемента");
                        int index = InputValidator.InputFromTO(0, collection.Count);
                        Product p = Change(collection[index]);
                        collection[index].ChangeProduct(p.Name,p.Price);
                        Actions(collection);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Продукт нельзя изменить");
                    }
                    break;
                case "5":
                    Console.WriteLine("Выберите магазин");
                    for (var i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        Console.WriteLine($"[{i}] {item.Name} Количество продуктов:{item.Count}");
                    }
                    GetMarket(Select());
                    break;
                default:
                    Console.WriteLine("Выбран неверный элемент");
                    Actions(collection);
                    break;
               
            }
        }
        

       
        /// <summary>
        /// Генерация магазинов
        /// </summary>
        public static void Markerts()
        {
            Random random = new Random();
            for (int i = 0; i < 2; i++)
                list.Add(new MyNewCollection(Generator.Generator.gen()));
            foreach (var item in list)
                IncludeDependences(item);
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                Console.WriteLine($"[{i}] {item.Name} Количество продуктов:{item.Count}");
            }
        }
        
        /// <summary>
        /// Включение подписок на события
        /// </summary>
        /// <param name="collection"></param>
        static void IncludeDependences(MyNewCollection collection)
        {
            collection.CollectionProductChanged += new CollectionHandler(journal.CollectionProductChanged);
            collection.DiscountProductIsEnd += new Discounter(journal.ProductStorageLifeEnd);
            collection.CollectionProductCountChanged += new CollectionHandler(journal.CollectionProductCountChanged);
            FoodProduct.DiscountChange += new Discounter(journal.ProductDiscountChanged);
        }
        /// <summary>
        /// Заполненик коллекции продуктами
        /// </summary>
        /// <param name="coll"></param>
        static void FillCollection(MyNewCollection coll)
        {
            List<Product> prod = Generator.Generator.generate();
            for (int i = 0; i < prod.Count; i++)
            {
                coll.Add(prod[i]);
            }
        }
       
        

        public static void Startmenu()
        {
            Console.WriteLine("Открыть уже созданые магазины 1-да, 2-нет");
            switch (Console.ReadLine())
            {
                case "1":
                    FilesInDirectory(@"C:\Users\vinog\source\repos\Lab16\Lab16\bin\Debug\Stores");
                    Console.WriteLine("Введите путь для получения данных");
                    list = serializator.Deserializator(@Console.ReadLine());//Получаем данные из файла
                    for (int i = 0; i < list.Count; i++)
                        IncludeDependences(list[i]);
                    break;
                case "2":
                    Markerts();//Создаем новые магазины
                    break;
                default:
                    Console.WriteLine("Выбрано неверный вариант");
                    Startmenu();
                    break;

            }
            
        }

        private static void FilesInDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
            }
        }
    }
    
}
