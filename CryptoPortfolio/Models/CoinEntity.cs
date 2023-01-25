namespace CryptoPortfolio.Models
{
    public class CoinEntity
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Current_Price { get; set; }
        public float ATH { get; set; }
    }
}
