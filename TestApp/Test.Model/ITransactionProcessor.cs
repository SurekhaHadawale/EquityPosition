using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model.ExternalModel;

namespace Test.Model
{
    /// <summary>
    /// The main abstraction to process transaction
    /// </summary>
    public interface ITransactionProcessor
    {
        void ProcessTransaction(IList<TransactionModel> transactionModels);
        IList<Position> GetPositions();

        IList<Transaction> GetTransactions();
    }
}