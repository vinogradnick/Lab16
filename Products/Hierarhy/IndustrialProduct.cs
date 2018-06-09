using System;

namespace Products
{

    [Serializable]
    public class IndustrialProduct : FoodProduct
    {
        public int Weight;
        public int Length;

        public IndustrialProduct(string name, int price, int weight, int length,DateTime timeprod,int life) : base(name, price,timeprod,life)
        {
            Weight = weight;
            Length = length;
        }

        public IndustrialProduct(string v) : base()
        {

        }

        protected IndustrialProduct()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => base.ToString() + $"Вес продукта:{Weight}\nДлина продукта:{Length}\n";
    }

}
