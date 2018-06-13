using System;
using Validator;

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
        public override  void ChangeProduct()
        {
             base.ChangeProduct();
            Console.WriteLine("Введите вес продукта");
            _weight = InputValidator.InputPositive();
            Console.WriteLine("Введите длину продукта");
            _length = InputValidator.InputPositive();
        }
        public override string ToString() => base.ToString() + $"Вес продукта:{_weight}\nДлина продукта:{_length}\n";
    }

}
