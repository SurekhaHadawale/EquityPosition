using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Model.ExternalModel;

namespace Test.TradeProcessor
{
    /// <summary>
    /// Concrete implementation of ITransactionProcessor.
    /// </summary>
    public class TransactionProcessor : ITransactionProcessor
    {
        public void ProcessTransaction(IList<TransactionModel> transactionModels)
        {
            //Process the transactions for each security in order of TradeAction
            transactionModels = transactionModels.OrderBy(x => x.SecurtiyCode).ThenBy(y => y.TradeAction).ToList();

            foreach (var item in transactionModels)
            {
                CreateTradeProcessor(item.TradeAction)?.ProcessTrade(item);
            }
        }

        public IList<Position> GetPositions()
        {
            return DataStore.DataStore.GetInstance().Position;
        }


        //This is additional method for display input Transactions
        public IList<Transaction> GetTransactions()
        {
            return DataStore.DataStore.GetInstance().Transaction;
        }


        /// <summary>
        /// Factory method to create specific type of Trade processor. This can be in future done via Dependency Injection.
        /// </summary>
        /// <param name="tradeActionEnum"></param>
        /// <returns></returns>
        private ITradeProcessor CreateTradeProcessor(TradeActionEnum tradeActionEnum)
        {
            ITradeProcessor tradeProcessor = null;

            switch (tradeActionEnum)
            {
                case TradeActionEnum.Insert:
                    tradeProcessor = new InsertTradeProcessor();
                    break;
                case TradeActionEnum.Update:
                    tradeProcessor = new UpdateTradeProcessor();
                    break;
                case TradeActionEnum.Cancel:
                    tradeProcessor = new CancelTradeProcessor();
                    break;
                default:
                    tradeProcessor = new InsertTradeProcessor();
                    break;
            }
            return tradeProcessor;
        }
    }
}