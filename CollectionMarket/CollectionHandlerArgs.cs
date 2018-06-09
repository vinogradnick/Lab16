using System;

namespace CollectionMarket
{
    public class CollectionHandlerArgs:EventArgs
    {
        public string Name;
        public string TypeChange { get; set; }
        public Object product { get; set; }

        public override string ToString() =>
            $"Название{this.Name} Тип события:{this.TypeChange} Объект:{this.product.ToString()}";

        public CollectionHandlerArgs(string name,string type,object obj)
        {
            Name = name;
            TypeChange = type;
            product = obj;
        }
    }
}