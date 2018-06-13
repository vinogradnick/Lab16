using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Products;

namespace CollectionMarket
{
    [Serializable]
    public class MyNewCollection : MyCollection<Product>
    {
        public string Name { get; private set; }
        public event CollectionHandler CollectionProductChanged;
        public event Discounter DiscountProductIsEnd;
        public event CollectionHandler CollectionProductCountChanged;
        public int backups = 1;
        public MyNewCollection(string name):base() => Name = name;

        protected virtual void OnCollectionProductChanged(CollectionHandlerArgs args) => CollectionProductChanged?.Invoke(this, args);
        /// <summary>
        /// Добавление товаров
        /// </summary>
        /// <param name="value"></param>
        public void Add(Product value)
        {
            OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name,"Товар добавлен",value));
            base.Add(value);
        }
        public void Add(IOrderedEnumerable<Product> sorted)
        {
            foreach (var product in sorted)
            {
                OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name, "Товар добавлен", product));
                base.Add(product);
            }
           
        }

        public void Work()
        {
            foreach (FoodProduct item in this)
            {
                item.Check();
                if (item.ProductLifeIsDead)
                        OffsProduct(item);
            }
        }
        public void Print()
        {
            Console.WriteLine("");
            Console.WriteLine($"{this.Name} Количество продуктов{this.Count}\n");
            for (int index = 0; index < this.Count; index++)
                Console.WriteLine($"[{index}] {this[index].ToString()}");
        }
        public  void Remove(Product product)
        {
            OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name,"Удаление товара",product));
            base.Remove(product);
        }

        public void OffsProduct(Product product)
        {
            if (this.Contains(product))
                OnCollectionProductChanged(new CollectionHandlerArgs(this.Name, "Списание товара", product));
            else
                Console.WriteLine("Данный продукт нельзя списать, если его нет");
        }

        public void RemoveOffered()
        {
            try
            {
                foreach (FoodProduct item in this)
                    if (item.ProductLifeIsDead)
                    {
                        OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name, "Удаление списаного товара", item));
                        this.Remove(item);
                    }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Remove(FoodProduct product)
        {
            OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name, "Удаление товара", product));
            base.Remove(product);
        }

        public void ChangeProduct(Product product)
        {
            try
            {
                array[this.GetIndex(product)] = product;

            }
            catch (Exception e)
            {
                Console.WriteLine("Продукта нет в списке");
            }
            OnCollectionProductChanged(new CollectionHandlerArgs(this.Name,"Изменение продукта",product));
            Console.WriteLine("Данный продукт нельзя изменить, если его нет");
        }

        public int GetIndex(Product p)
        {
            
            for (int i = 0; i < array.Length; i++)
            {
                if (p == array[i])
                    return i;
            }

            return -1;
        }

       
        /// <summary>
        /// Сортировка по цене
        /// </summary>
        public void SortByPrice()
        {
            try
            {
                IOrderedEnumerable<Product> sorted = from product in array orderby product.Price select product;
                this.array = sorted.ToArray();
            }
            catch (Exception e)
            {
               Console.WriteLine("Невозможно отсортировать объекты которых нет");
            }
          
        }
        /// <summary>
        /// Вывод на экран всей еды
        /// </summary>
        public void PrintAllFood()
        {
            var item = from product in array where product.GetType() == typeof(FoodProduct) select product;
            foreach (var el in item)
                Console.WriteLine(el.ToString());
        }
        protected virtual void OnDiscountProductIsEnd(object obj, DiscountHandler evehandler) => DiscountProductIsEnd?.Invoke(obj, evehandler);
        protected virtual void OnCollectionProductCountChanged(CollectionHandlerArgs args) => CollectionProductCountChanged?.Invoke(this, args);
    }

    public delegate void CollectionHandler(object sender, CollectionHandlerArgs args);
}
