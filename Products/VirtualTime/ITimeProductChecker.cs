namespace Products
{
    public interface ITimeProductChecker
    {
        int PredictionTime { get; set; }
        void Check();
    }
}