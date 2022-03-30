namespace Test.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TradeId { get; set; }
        public int Version { get; set; }
        public TradeSecurity SecurityCode { get; set; }
        public decimal Quantity { get; set; }
        public TradeActionEnum TradeAction { get; set; }
        public TradeActivityEnum TradeActivity { get; set; }
    }
}