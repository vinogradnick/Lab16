using System;

namespace Products
{
    [Serializable]
    public class ConstructionProduct :IndustrialProduct
    {
        private string _material;
        private string _typeMaterial;

        private ConstructionProduct() : base()
        {

        }

        public ConstructionProduct(string name, int price, string material, string typeMaterial,int weight,int length,DateTime timeprod,int life) : base(name,price,weight,length,timeprod,life)
        {
            _material = material;
            _typeMaterial = typeMaterial;
        }

        public  void ChangeProduct(string name,int price,int storage,int weight,int length,string material,string _type)
        {
            _material = material;
            _typeMaterial = _type;
            base.ChangeProduct(name, price);
        }

        public override string ToString() => base.ToString() + $"Материал {_material}\nТип материала{_typeMaterial}\n";

        
    }

}
