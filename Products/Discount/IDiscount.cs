namespace Products
{
    public delegate void Discounter(object obj, DiscountHandler eveHandler);
    public interface IDiscount
    {
     
        /// <summary>
        /// Процент по скидке
        /// </summary>
        double PercentDiscount { get; set; }
       
        void OnDiscountChange(DiscountHandler handler);
    }
}
