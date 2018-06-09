using System;

namespace Products
{
    public class DiscountHandler : EventArgs
    {
        public string Message { get; set; }
        public double Price { get; set; }
        public double Discount;
        public int StorageLife { get; set; }
        public Object Product;

        public DiscountHandler(string message, double price, double discount, object product)
        {
            Message = message;
            Price = price;
            Discount = discount;
            this.Product = product;
        }

    }

}
