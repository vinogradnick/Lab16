using System;

namespace Products
{
    [Serializable]
    public class ConstructionProduct :IndustrialProduct
    {
        public string Material;
        public string TypeMaterial;

        public ConstructionProduct() : base()
        {

        }

        public ConstructionProduct(string name, int price, string material, string typeMaterial,int weight,int length,DateTime timeprod,int life) : base(name,price,weight,length,timeprod,life)
        {
            Material = material;
            TypeMaterial = typeMaterial;
        }
        public void MakeDiscount()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => base.ToString() + $"Материал {Material}\nТип материала{TypeMaterial}\n";

        public void IncreasePrice()
        {
            throw new NotImplementedException();
        }
    }

}
