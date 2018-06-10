﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml.Serialization;
using CollectionMarket;
using Products;


namespace Serializatior
{
    public  class Serializator
    {
        public string Path;
        public static int backups = 1;
        public Serializator()
        {

        }

        public void Serialize(MyNewCollection collection,string colname,Journal journal)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {

                FileStream fs = new FileStream($"{colname}.dat",FileMode.OpenOrCreate);
                formatter.Serialize(fs,collection);
                fs.Close();
                fs.Dispose();                
                Backup(colname);
                SaveToAccess(collection.Name,collection,journal);
                backups++;

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
                Thread.Sleep(10);

            }
            Console.WriteLine();
            Console.WriteLine("Информация сохранена " + info + ".dat");

        }

        public void SaveJournal(Journal journal)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void SaveToAccess(string about,MyNewCollection productCollection, Journal journal)
        {
            try
            {
                OleDbConnection connection =
                    new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Market.mdb");
                connection.Open();
                OleDbCommand command;              
                if (backups ==1)
                {
                    string query =
                        $"INSERT INTO [Market] ([market_name],[count_products],[count_backups],[count_offedproducts]) VALUES ('{productCollection.Name}',{productCollection.Count},{backups},{journal.DeadProducts.Count})";

                    command = new OleDbCommand(query, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    foreach (var product in productCollection)
                    {
                        var item = (FoodProduct) product;
                        string add =
                            $"INSERT INTO [Products] ( [name_product],[storage_life],[price],[date_production],[discount],[dead],[market_name] ) VALUES ( '{item.Name}',{item.StorageLife},{(int)item.Price},'{item.DateProduction}',{item.PercentDiscount},{item.ProductLifeIsDead},'{productCollection.Name}')";
                       
                        command = new OleDbCommand(add, connection);

                        command.ExecuteNonQuery();
                    }
                    command.Dispose();
                }
                else
                {
                    string query =
                        $"UPDATE Market SET count_backups ={backups},count_products ={productCollection.Count},count_offedproducts ={journal.DeadProducts.Count} WHERE market_name ='{productCollection.Name}'";
                  command =new OleDbCommand(query,connection);
                  command.ExecuteNonQuery();
                    Console.WriteLine("Информация о магазине в базе данных обновлена");
                    foreach (var product in productCollection)
                    {
                        var item = (FoodProduct) product;
                        item.Check();

                        string add =
                            $"UPDATE Products SET storage_life={item.StorageLife},price={(int)item.Price},discount={item.PercentDiscount},dead={item.isOffed} WHERE name_product='{item.Name}'";
                        command = new OleDbCommand(add, connection);
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Информация о продуктах обновлена");
                    
                }
                Console.WriteLine("База данных сохранена в Market.mdb");
                connection.Close();
                connection.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
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