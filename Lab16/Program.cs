using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        static void Main(string[] args) => Game();

        public static void Game()
        {
            Startmenu();
            bool repeat = false;
            while (!repeat)
            {
                NextDay();
                SaveData();
                if (Console.ReadLine() == "R") repeat = true;
            }
            Console.WriteLine("День работы был завершнен");
            Console.ReadLine();

            serializator.SaveJournal(journal);//Сохранение информации о товаре в журнале
            Menu();//Вызов меню
            Choise();//Выбор вариантов
            Console.ReadLine();


        }
        /// <summary>
        /// Генерация магазинов
        /// </summary>
        public static void Markerts()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(1,10); i++)
                list.Add(new MyNewCollection(Generator.Generator.gen()));
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
                for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                    list[i].Remove(list[i][rd.Next(list[i].Count - 1)]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Продукты кончились");
                Console.WriteLine("Завозим продукты в магазин");
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.Write($"");
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
            Console.WriteLine("5-Выбрать магазин");
            
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

        public void ChoiseDirectory()
        {
            
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
            foreach (var item in list)
                IncludeDependences(item);

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
