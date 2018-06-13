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
            Game();
        }

        public static  void Game()
        {
            Startmenu();
            NextDay();
            SaveData();
            int index = InputValidator.InputPositive();
            GetMarket(list[index]);


        }

        public static void GetMarket(MyNewCollection collection)
        {
            Console.WriteLine("Выбран магазин "+collection.Name);
            collection.Print();
            Console.WriteLine("1-Работа с продуктами");
            Console.WriteLine("2-Очистить магазин");
            Console.WriteLine("3-Добавить продукты в магазин");
            CollectionAction(collection);
        }

        public static void CollectionAction(MyNewCollection collection)
        {
            switch (Console.ReadLine())
            {
                case "1":
                    Actions(collection);
                    break;
                case "2":
                    collection.Clear();
                    break;
                case "3":
                    FillCollection(collection);
                    break;
                default:
                    Console.WriteLine("Вы выбрали неверный ответ");
                    CollectionAction(collection);
                    break;
            }
        }

        public static void Actions(MyNewCollection collection)
        {
            collection.Print();
            Console.WriteLine("1- Выбрать товар");
            Console.WriteLine("2- Сортировка товара по цене");
            Console.WriteLine("3- Удалить товар");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Введите индекс товара");
                    try
                    {
                        int index = InputValidator.InputPositive();
                        Product product = collection[index];
                        collection.ChangeProduct(index,product);
                    }
                    catch (Exception e)
                    {
                       Console.WriteLine("Товара нет в списке");
                        
                    }
                    
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
                        int index = InputValidator.InputPositive();
                        Product product = collection[index];
                        collection.Remove(product);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Товара нельзя удалить");
                    }
                    break;
                default:
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
            foreach (var item in list)
                Console.WriteLine($"{item.Name} Количество продуктов:{item.Count}");
        }
        public static void SaveData()
        {
            foreach (MyNewCollection item in list)
            {
                item.RemoveOffered();
                string path = $@"C:\Users\vinog\source\repos\Lab16\Lab16\bin\Debug\Магазины\{item.Name}";
                Directory.CreateDirectory(path);
                serializator.Serialize(item, $@"{path}\{item.Name}___{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}", journal);
            }

            string save =
                $@"C:\Users\vinog\source\repos\Lab16\Lab16\bin\Debug\Stores\Магазины_{DateTime.Now.Hour}_{
                        DateTime.Now.Minute
                    }_Количество_{list.Count}";
            serializator.SerializeAllMarkets(list,save);
            list = serializator.Deserializator(save);
            MarkertsRemove();
        }

        public static void NextDay()
        {
            foreach (MyNewCollection item in list)
                item.Print();

            foreach (MyNewCollection item in list)
                item.Work();
            Console.WriteLine("Перейти на следующий день час работы");
        }
        
        /// <summary>
        /// Удаление элементов из магазина
        /// </summary>
        public static void MarkertsRemove()
        {
            Random rd = new Random();
            try
            {
                foreach (var t in list)
                        t.Remove(t[rd.Next(t.Count - 1)]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Продукты кончились");
                Console.WriteLine("Завозим продукты в магазин");
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.Write($"|");
                }
                foreach (var item in list)
                    FillCollection(item);
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
            collection.Add(new IndustrialProduct("быдых", 100, 1000, 10, DateTime.Now, 20));
           FillCollection(collection);
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
        public static void Menu()
        {
            
            Console.WriteLine("Вас приветствует магазин продуктов");
            Console.WriteLine("1-Выбрать файл с данными за определенные дни");
            Console.WriteLine("2-Посмотреть продукты");
            Console.WriteLine("3-Посмотреть журнал событий");
            Console.WriteLine("4-Выбрать магазин");
            
        }

        public static void Choise()
        {
            switch (Console.ReadLine())
            {
               case "1":
                   
                   break;
                case "2":
                    System.Diagnostics.Process.Start("Market.mdb");
                    break;
                case "3":
                    break;
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

        public static void FilesInDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
            }
        }
    }
    
}
