using System;

namespace Products
{

    [Serializable]
    public class IndustrialProduct : FoodProduct
    {
        private int _weight;
        private int _length;

        public IndustrialProduct(string name, int price, int weight, int length,DateTime timeprod,int life) : base(name, price,timeprod,life)
        {
            _weight = weight;
            _length = length;
        }


        protected IndustrialProduct(){}

        public virtual void ChangeProduct(string name,int price,int storage,int weight,int length)
        {
            this._weight = weight;
            this._length = length;
            base.ChangeProduct(name,price,storage);
        }
        public override string ToString() => base.ToString() + $"Вес продукта:{_weight}\nДлина продукта:{_length}\n";
    }

}
