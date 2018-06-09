using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml.Serialization;
using CollectionMarket;


namespace Serializatior
{
    public  class Serializator
    {
        public string Path;
        
        public Serializator()
        {

        }

        public void Serialize(MyNewCollection collection,string colname)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {

                FileStream fs = new FileStream($"{colname}.dat",FileMode.OpenOrCreate);
                formatter.Serialize(fs,collection);
                fs.Close();
                fs.Dispose();                
                Backup(colname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Backup(string info)
        {
            Console.WriteLine("");
            Console.WriteLine($"Резервное копирование включено| Текущее время {DateTime.Now}");
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("|");
                Thread.Sleep(50);

            }
            Console.WriteLine();
            Console.WriteLine("Информация сохранена " + info + ".dat");

        }
        public void Serialize(Journal journal)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                FileStream fs = new FileStream($"Журнал{DateTime.Now.Ticks}.dat", FileMode.OpenOrCreate);
                formatter.Serialize(fs, journal);
                Console.WriteLine("Информация сохранена в" );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void SortSerialize(MyNewCollection collection,string typefile)
        {
            try
            {
                FileStream fs = new FileStream($"{typefile}.xml", FileMode.OpenOrCreate);
               
                fs.Close();
                fs.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public MyNewCollection Deserializator(string file)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                FileStream fs = new FileStream($"{file}.xml", FileMode.OpenOrCreate);
                MyNewCollection collection = (MyNewCollection)formatter.Deserialize(fs);
                fs.Close();
                fs.Dispose();
                return collection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}