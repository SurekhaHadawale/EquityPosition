using System.Linq;
using Test.Model;
using Test.Model.ExternalModel;

namespace Test.TradeProcessor
{
    /// <summary>
    /// Encapsulates how to handle update of trade as per current Rules.
    /// </summary>
    public class UpdateTradeProcessor : BaseTradeProcessor
    {
        public override void ProcessTrade(TransactionModel transactionModel)
        {
            //Get Transaction for incoming SecurityCode, TradeId with Insert activity
            var transactions = DataStore.DataStore.GetInstance().Transaction.Where(x => x.SecurityCode.SecurityCode == transactionModel.SecurtiyCode &&
                                            x.TradeId == transactionModel.TradeId);

            var insertTransaction = transactions?.SingleOrDefault(x => x.TradeAction == Model.TradeActionEnum.Insert);

            if (insertTransaction != null)
            {
                var newTransaction = new Transaction();
                newTransaction.TransactionId = GetNextTransactionId();
                newTransaction.TradeId = transactionModel.TradeId;
                newTransaction.Version = insertTransaction.Version + 1;
                newTransaction.SecurityCode = new TradeSecurity { SecurityCode = transactionModel.SecurtiyCode };
                newTransaction.Quantity = transactionModel.Quantity;
                newTransaction.TradeAction = transactionModel.TradeAction;
                newTransaction.TradeActivity = transactionModel.TradeActivity;

                DataStore.DataStore.GetInstance().Transaction.Add(newTransaction);
            }

            //Process Position
            var position = DataStore.DataStore.GetInstance().Position.SingleOrDefault(x => x.SecurityCode.SecurityCode == transactionModel.SecurtiyCode);

            position.Quantity = transactionModel.TradeActivity == TradeActivityEnum.Buy ? transactionModel.Quantity
                : position.Quantity - transactionModel.Quantity;
        }
    }
}