using System.Linq;
using Test.Model;
using Test.Model.ExternalModel;

namespace Test.TradeProcessor
{
    /// <summary>
    /// Abstract class to implememt common Trade processing logic.
    /// </summary>
    public abstract class BaseTradeProcessor : ITradeProcessor
    {
        public abstract void ProcessTrade(TransactionModel transactionModel);

        protected int GetNextTransactionId()
        {
            int nextTransactionId = 1;
            if (DataStore.DataStore.GetInstance().Transaction.Count > 0)
                nextTransactionId = DataStore.DataStore.GetInstance().Transaction.Max(x => x.TransactionId) + 1;

            return nextTransactionId;
        }      
    }
}