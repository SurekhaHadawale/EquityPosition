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
    /// Encapsulates how to handle insert of trade as per current Rules.
    /// </summary>
    public class InsertTradeProcessor : BaseTradeProcessor
    {
        public override void ProcessTrade(TransactionModel transactionModel)
        {
            //Process Transaction
            var transaction = new Transaction();
            transaction.TransactionId = GetNextTransactionId();
            transaction.TradeId = transactionModel.TradeId;
            transaction.Version = 1; //INSERT will always be 1st version of a Trade
            transaction.SecurityCode = new TradeSecurity { SecurityCode = transactionModel.SecurtiyCode };
            transaction.Quantity = transactionModel.Quantity;
            transaction.TradeAction = transactionModel.TradeAction;
            transaction.TradeActivity = transactionModel.TradeActivity;

            DataStore.DataStore.GetInstance().Transaction.Add(transaction);

            //Process Position
            var position = DataStore.DataStore.GetInstance().Position.SingleOrDefault(x => x.SecurityCode.SecurityCode == transactionModel.SecurtiyCode);

            if (position == null)
            {
                position = new Position();
                position.SecurityCode = new TradeSecurity { SecurityCode = transactionModel.SecurtiyCode };
                position.Quantity = transactionModel.TradeActivity == TradeActivityEnum.Buy ? transactionModel.Quantity : -1 * transactionModel.Quantity;               

                DataStore.DataStore.GetInstance().Position.Add(position);
            }
            else
            {
                position.Quantity = transactionModel.TradeActivity == TradeActivityEnum.Buy ? position.Quantity + transactionModel.Quantity
                : position.Quantity - transactionModel.Quantity;                
            }           
        }
    }
}