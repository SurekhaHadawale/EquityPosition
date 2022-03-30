using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Model.ExternalModel;

namespace Test.TradeService
{
    /// <summary>
    /// This is Service which client calls
    /// </summary>
    public class TradeService
    {
        private ITransactionProcessor transactionProcessor;

        public TradeService(ITransactionProcessor transactionProcessor)
        {
            this.transactionProcessor = transactionProcessor;
        }

        public void ProcessTransactions(List<TransactionModel> transactionModels)
        {
            transactionProcessor.ProcessTransaction(transactionModels);
        }

        public IList<Position> GetPositions()
        {
           return transactionProcessor.GetPositions();
        }

        public IList<Transaction> GetTransactions()
        {
            return transactionProcessor.GetTransactions();
        }
    }
}