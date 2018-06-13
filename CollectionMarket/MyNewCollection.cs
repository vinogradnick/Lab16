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
    public class MyNewCollection : MyCollection
    {
        public string Name { get; set; }
        public event CollectionHandler CollectionProductChanged;
        public event Discounter DiscountProductIsEnd;
        public event CollectionHandler CollectionProductCountChanged;
        public int backups = 1;
        public MyNewCollection(string name):base() => Name = name;

        protected virtual void OnCollectionProductChanged(CollectionHandlerArgs args) => CollectionProductChanged?.Invoke(this, args);

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
            foreach (var item in this)
                Console.WriteLine(item.ToString());
        }
        public  void Remove(Product product)
        {
            OnCollectionProductCountChanged(new CollectionHandlerArgs(this.Name,"Удаление товара",product));
            base.Remove(product);
        }

        public void OffsProduct(Product product)
        {
            if (this.Contains(product))
            {
                product.isOffed = true;
                OnCollectionProductChanged(new CollectionHandlerArgs(this.Name, "Списание товара", product));
            }
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

        public void ChangeProduct(int index,Product product)
        {
            this[index] = product;
            OnCollectionProductChanged(new CollectionHandlerArgs(this.Name,"изменение продукта",product));
                Console.WriteLine("Данный продукт нельзя изменить, если его нет");
        }

        public void SortByPrice()
        {
            var sorted = from product in this orderby product.Price select product;
            base.Clear();
            foreach (var item in sorted)
                base.Add(item);
        }
       
        
        protected virtual void OnDiscountProductIsEnd(object obj, DiscountHandler evehandler) => DiscountProductIsEnd?.Invoke(obj, evehandler);

        protected virtual void OnCollectionProductCountChanged(CollectionHandlerArgs args)
        {
            CollectionProductCountChanged?.Invoke(this, args);
        }
    }

    public delegate void CollectionHandler(object sender, CollectionHandlerArgs args);
}
