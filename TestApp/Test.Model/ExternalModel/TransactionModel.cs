namespace Test.Model.ExternalModel
{
    /// <summary>
    /// ViewModel for External world to seaparte core Transaction structure.
    /// </summary>
    public class TransactionModel
    {
        public int TradeId { get; set; }
        public string SecurtiyCode { get; set; }
        public TradeActivityEnum TradeActivity { get; set; }
        public TradeActionEnum TradeAction { get; set; }
        public decimal Quantity { get; set; }
    }    
}