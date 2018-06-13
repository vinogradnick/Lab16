using System;
using Validator;

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
        public bool ProductLifeIsDead
        {
            get => StorageLife == 0;
            set => throw new NotImplementedException();
        }
        public DateTime DateProduction
        {
            get => _dateProduction;
            set => _dateProduction = value;
        }
        public int StorageLife
        {
            get => _storageLife;
            set => _storageLife = value;
        }
        public FoodProduct():base()
        {

        }
        public FoodProduct(string name, int price, DateTime dateProduction, int storageLife) : base(name, price)
        {
            DateProduction = dateProduction;
            StorageLife = storageLife;
        }
        /// <summary>
        /// Изменение в продукте
        /// </summary>
        public override void ChangeProduct()
        {
            base.ChangeProduct();
            Console.WriteLine("Введите срок годности продукта");
            StorageLife = InputValidator.InputPositive();
        }
        public void OnDiscountChange(DiscountHandler handler) => DiscountChange?.Invoke(this, handler);

        public override string ToString() => base.ToString()+$"Дата производства:{DateProduction}\nСрок годности:{StorageLife}\nСкидка на товар:{PercentDiscount}\nПродукт испортился:{ProductLifeIsDead}\n";

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
