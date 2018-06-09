using System;

namespace Products
{
    [Serializable]
    public class FoodProduct : Product,IDiscount,ITimeProductChecker
    {
        public DateTime DateProduction { get; set; }

        public int StorageLife { get; set; }

        public FoodProduct():base()
        {

        }
        public FoodProduct(string name, int price, DateTime dateProduction, int storageLife) : base(name, price)
        {
            DateProduction = dateProduction;
            StorageLife = storageLife;
            PredictionTime = VirtualTimer.Time;
        }


        public static event Discounter DiscountChange;
        public double PercentDiscount { get; set; }
        public bool ProductLifeIsDead { get; set; }
       
        public void OnDiscountChange(DiscountHandler handler)
        {
            DiscountChange?.Invoke(this, handler);
        }


        public override string ToString() => base.ToString()+$"Дата производства:{DateProduction}\nСрок годности:{StorageLife}\nСкидка на товар:{PercentDiscount}\nПродукт испортился:{ProductLifeIsDead}\n";

        
        public int PredictionTime { get; set; }

        public void Check()
        {
            if (StorageLife == 0)
            {
                ProductLifeIsDead = true;
                return;
            }
            else
                StorageLife--;
            if (ProductLifeIsDead == false)
            {
                if (StorageLife > 5)
                    return;
                if (StorageLife < 5)
                {
                    PercentDiscount = 35;
                    Price-= Price * (PercentDiscount / 100);
                    OnDiscountChange(new DiscountHandler("Товар скоро испортится", Price, PercentDiscount, this));
                }
                if (StorageLife < 2)
                {
                    PercentDiscount = 50;
                    Price-= Price * (PercentDiscount / 100);
                    OnDiscountChange(new DiscountHandler("Товар скоро испортится", Price, PercentDiscount, this));

                }
                Price-= Price * (PercentDiscount / 100);
                OnDiscountChange(new DiscountHandler("Товар скоро испортится", Price, PercentDiscount, this));
            }
                
        }
    }

}
