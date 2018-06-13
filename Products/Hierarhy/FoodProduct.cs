using System;

namespace Products
{
    [Serializable]
    public class FoodProduct : Product,IDiscount
    {
        private DateTime _dateProduction;
        private int _storageLife;
        private double _percentDiscount;
        public static event Discounter DiscountChange;

        public double PercentDiscount
        {
            get => _percentDiscount;
            set => _percentDiscount = value;
        }

        public bool ProductLifeIsDead => StorageLife == 0;

        public DateTime DateProduction
        {
            get => _dateProduction;
            private set => _dateProduction = value;
        }

        public int StorageLife
        {
            get => _storageLife;
            private set => _storageLife = value;
        }

        protected FoodProduct():base()
        {

        }
        public FoodProduct(string name, int price, DateTime dateProduction, int storageLife) : base(name, price)
        {
            DateProduction = dateProduction;
            StorageLife = storageLife;
        }

        public virtual void ChangeProduct(string name,int price,int storage)
        {
            StorageLife = storage;
            base.ChangeProduct(name,price);
        }


        
        public void OnDiscountChange(DiscountHandler handler)
        {
            DiscountChange?.Invoke(this, handler);
        }


        public override string ToString() => base.ToString()+$"Дата производства:{DateProduction}\nСрок годности:{StorageLife}\nСкидка на товар:{PercentDiscount}\nПродукт испортился:{ProductLifeIsDead}\n";

        public void Check()
        {
            if (StorageLife == 0)
            {
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
