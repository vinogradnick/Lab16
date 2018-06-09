using System;
using System.Collections;

namespace Products
{
    [Serializable]
    public abstract class Product:IComparable,IComparer
    {
        public string NameProduct;
        public static int ID => id;

        public static int id = 0;

        public string Name { get; set; }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new Exception("Цена товара не может быть больше 0");
                _price = value;
            }
        }

        public bool isOffed { get; set; }

        private double _price;
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        protected Product(string name,int price)
        {
            id++;
            Name = name;
            Price = price;
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        protected Product()
        {
            id++;
        }
        public int CompareTo(object obj)
        {
            Product product = (Product)obj;
            return this.Price > product.Price ? 1 : (this.Price < product.Price ? -1 : 0);
        }

        public object Clone<T>() where T : new()
        {
            T t = new T();
            return t;
        }

        public override string ToString() => $"ID:{id}\nТип:{this.GetType()}\nНазвание продукта:{Name}\nСтоимость продукта:{Price}\n";

        public int Compare(object x, object y)
        {
            Product p1 = (Product) x;
            Product p2 = (Product) y;
            return p1.Price > p2.Price ? 1 : (p1.Price < p2.Price ? -1 : 0);
        }

        
    }

}
