using System;

namespace Products
{
    [Serializable]
    public class ConstructionProduct :IndustrialProduct
    {
        private string _material;
        private string _typeMaterial;

        public ConstructionProduct() : base()
        {

        }

        public ConstructionProduct(string name, int price, string material, string typeMaterial,int weight,int length,DateTime timeprod,int life) : base(name,price,weight,length,timeprod,life)
        {
            _material = material;
            _typeMaterial = typeMaterial;
        }
        public void MakeDiscount()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => base.ToString() + $"Материал {_material}\nТип материала{_typeMaterial}\n";

        public void IncreasePrice()
        {
            throw new NotImplementedException();
        }
    }

}
