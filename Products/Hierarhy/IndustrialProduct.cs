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

        public IndustrialProduct(string v) : base()
        {

        }

        protected IndustrialProduct()
        {
            throw new NotImplementedException();
        }

        public void ChangeProduct()
        {
            
        }
        public override string ToString() => base.ToString() + $"Вес продукта:{_weight}\nДлина продукта:{_length}\n";
    }

}
