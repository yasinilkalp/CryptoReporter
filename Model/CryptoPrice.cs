using System;

namespace CryptoReporter.Model
{
    public class CryptoPrice
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get { return Price.ToString("N2"); } }
        public string Symbol { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Date
        {
            get
            {
                return Timestamp.HasValue ? Timestamp.Value.ToString("dd MMMM yyyy") : DateTime.Now.ToString("dd MMMM yyyy");
            }
        }
    }
}
