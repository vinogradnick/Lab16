using System;

namespace Products
{
    [Serializable]
    public class ConstructionProduct :IndustrialProduct
    {
        private string _material;
        private string _typeMaterial;

        

        public ConstructionProduct(string name, int price, string material, string typeMaterial,int weight,int length,DateTime timeprod,int life) : base(name,price,weight,length,timeprod,life)
        {
            _material = material;
            _typeMaterial = typeMaterial;
        }


        public override void ChangeProduct()
        {
            base.ChangeProduct();
            Console.WriteLine("Введите материал");
            _material = Console.ReadLine();
            Console.WriteLine("Введите тип материал");
            _typeMaterial = Console.ReadLine();
        }

        public override string ToString() => base.ToString() + $"Материал {_material}\nТип материала{_typeMaterial}\n";

        public void IncreasePrice()
        {
            throw new NotImplementedException();
        }
    }

}
