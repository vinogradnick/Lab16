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
        static MyCollection<MyNewCollection> list = new MyCollection<MyNewCollection>();
        static void Main(string[] args)
        {
            Startmenu();
            GetMarket(Select());
        }
        /// <summary>
        /// Выбор магазина коллекции по индексу элемента
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Изменение продукта по индексу элемента 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product Change(Product product)
        {
             product.ChangeProduct();
            return product;
        }
        /// <summary>
        /// Сохранение данных магазинов 
        /// </summary>
        public static void Save()
        {
            serializator.SerializeAllMarkets(list, $@"C:\Users\vinog\source\repos\Lab16\Lab16\bin\Debug\Stores\Магазин_{DateTime.Now.Hour}_{DateTime.Now.Minute}");
            serializator.SaveJournal(journal);
        }
        /// <summary>
        /// Получить магазин
        /// </summary>
        /// <param name="collection"></param>
        private static void GetMarket(MyNewCollection collection) => CollectionAction(collection);

        /// <summary>
        /// Работа с магазином
        /// </summary>
        /// <param name="collection"></param>
        private static void CollectionAction(MyNewCollection collection)
        {
            Save();
            Console.WriteLine("Выбран магазин "+collection.Name);
            collection.Print();
            Console.WriteLine("1-Работа с продуктами");
            Console.WriteLine("2-Очистить магазин");
            Console.WriteLine("3-Добавить продукты в магазин");
            Console.WriteLine("4-Вернуться назад");
          
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
                    Save();
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
            Save();
            collection.Print();
            Console.WriteLine("1-Добавить продукт");
            Console.WriteLine("2-Сортировка товара по цене");
            Console.WriteLine("3-Удалить товар");
            Console.WriteLine("4-Изменить товар");
            Console.WriteLine("5-Печать всех продуктов класса FoodProduct");
            Console.WriteLine("6-Вернуться назад");
            switch (Console.ReadLine())
            {
                case "1":
                    Product el = Generator.Generator.generate()[0];
                    Console.WriteLine(el.ToString());
                    collection.Add(el);
                    Actions(collection);
                    break;
                case "2":
                    collection.SortByPrice();
                    collection.Print();
                    Actions(collection);
                    serializator.SaveJournal(journal);
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
                        Console.WriteLine(collection[index].ToString());
                        Product p = Change(collection[index]);
                        collection.ChangeProduct(collection[index]);
                        Actions(collection);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Продукт нельзя изменить");
                    }
                    break;
                case "5":
                    collection.PrintAllFood();
                    Actions(collection);
                    break;
                case "6":
                    Save();
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
            for (int i = 0; i < 3; i++)
                list.Add(new MyNewCollection(Generator.Generator.gen()));
            foreach (MyNewCollection item in list)
            {
                IncludeDependences(item);//Включение подписки на события
                FillCollection(item);//Заполнение коллекции
            }

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
        /// <summary>
        /// 
        /// </summary>
        public static void Startmenu()
        {
            Console.WriteLine("Открыть уже созданые магазины 1-да, 2-нет");
            switch (Console.ReadLine())
            {
                case "1":
                    list = serializator.Deserializator(FilesInDirectory(@"C:\Users\vinog\source\repos\Lab16\Lab16\bin\Debug\Stores"));//Получаем данные из файла
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
        /// <summary>
        /// Получить файл пути из директории
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string FilesInDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            for (var index = 0; index < files.Length; index++)
            {
                var file = files[index];
                Console.WriteLine($"[{index}] = {file}");
            }
            Console.WriteLine("Выберите элемент");
            return files[InputValidator.InputFromTO(0, files.Length)];
        }
    }
    
}
