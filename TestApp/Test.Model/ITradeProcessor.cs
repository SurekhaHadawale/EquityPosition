using Test.Model.ExternalModel;

namespace Test.Model
{
    public interface ITradeProcessor
    {
        void ProcessTrade(TransactionModel transactionModel);
    }
}