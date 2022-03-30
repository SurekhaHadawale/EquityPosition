using System.Linq;
using Test.Model;
using Test.Model.ExternalModel;

namespace Test.TradeProcessor
{
    /// <summary>
    /// Encapsulates how to handle cancel of trade as per current Rules.
    /// </summary>
    public class CancelTradeProcessor : BaseTradeProcessor
    {
        public override void ProcessTrade(TransactionModel transactionModel)
        {
            //Get Transaction for incoming SecurityCode, TradeId with Insert activity
            var transactions = DataStore.DataStore.GetInstance().Transaction.Where(x => x.SecurityCode.SecurityCode == transactionModel.SecurtiyCode &&
                                            x.TradeId == transactionModel.TradeId);

            var newTransaction = new Transaction();
            newTransaction.TradeId = transactionModel.TradeId;
            newTransaction.TransactionId = GetNextTransactionId();
            newTransaction.Version = transactions.Any() ? transactions.Max(x => x.Version) + 1 : 1;
            newTransaction.SecurityCode = new TradeSecurity { SecurityCode = transactionModel.SecurtiyCode };
            newTransaction.Quantity = transactionModel.Quantity;
            newTransaction.TradeAction = transactionModel.TradeAction;
            newTransaction.TradeActivity = transactionModel.TradeActivity;

            DataStore.DataStore.GetInstance().Transaction.Add(newTransaction);

            //Rule: For CANCEL, any changes in SecurityCode or Quantity or Buy/Sell may change and should be ignored
            //So, no processing of Position for Cancel Transaction            
        }
    }
}