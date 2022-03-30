using System;
using System.Collections.Generic;
using System.Linq;
using Test.Model;
using Test.Model.ExternalModel;
using Test.TradeProcessor;
using Test.TradeService;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client would call the service to process Tranactions. 
            //The service & business layer can be instantiated using dependency injection
            //For time being inputs are given from console client

            ITransactionProcessor transactionProcessor = new TransactionProcessor();
            TradeService tradeService = new TradeService(transactionProcessor);

            ProcessTransactions(tradeService);
            GetPositions(tradeService);
        }

        //ProcessTransactions
        static void ProcessTransactions(TradeService tradeService)
        {
            List<TransactionModel> transactionModel = new List<TransactionModel>()
            {
                new TransactionModel {TradeId = 1, SecurtiyCode = "REL", Quantity = 50, TradeAction = TradeActionEnum.Insert,  TradeActivity = TradeActivityEnum.Buy},
                new TransactionModel {TradeId = 2, SecurtiyCode = "ITC", Quantity = 40, TradeAction = TradeActionEnum.Insert,  TradeActivity = TradeActivityEnum.Sell},
                new TransactionModel {TradeId = 3, SecurtiyCode = "INF", Quantity = 70, TradeAction = TradeActionEnum.Insert,  TradeActivity = TradeActivityEnum.Buy},
                new TransactionModel {TradeId = 1, SecurtiyCode = "REL", Quantity = 60, TradeAction = TradeActionEnum.Update,  TradeActivity = TradeActivityEnum.Buy},
                new TransactionModel {TradeId = 2, SecurtiyCode = "ITC", Quantity = 30, TradeAction = TradeActionEnum.Cancel,  TradeActivity = TradeActivityEnum.Buy},
                new TransactionModel {TradeId = 4, SecurtiyCode = "INF", Quantity = 20, TradeAction = TradeActionEnum.Insert,  TradeActivity = TradeActivityEnum.Sell},
            };

            tradeService.ProcessTransactions(transactionModel);

            //Print input Transactions
            var transactions = tradeService.GetTransactions().OrderBy(x => x.TransactionId).ThenBy(y => y.TradeId).ThenBy(z=> z.Version).ToList();

            Console.WriteLine("Transactions: ");         


            foreach (var itemModel in transactionModel)
            {
                var item = transactions.FirstOrDefault(x => x.SecurityCode.SecurityCode == itemModel.SecurtiyCode &&
                           x.TradeId == itemModel.TradeId && x.TradeAction == itemModel.TradeAction && x.TradeActivity == itemModel.TradeActivity );

                Console.WriteLine($"TradeId: {item.TradeId}, Vesrion: {item.Version},  SecurityCode: {item.SecurityCode.SecurityCode}, " +
                    $" Quantity: {item.Quantity}, Action: {item.TradeAction}, Activity: {item.TradeActivity} ");
            }

            Console.WriteLine();
            Console.WriteLine("*******************************");
            Console.WriteLine();
        }

        //GetPositions
        static void GetPositions(TradeService tradeService)
        {
            var positions = tradeService.GetPositions();

            Console.WriteLine("Positions: ");
            string strQty = string.Empty;

            foreach (var item in positions)
            {
                if (item.Quantity > 0)
                    strQty = $"+{item.Quantity}";
                else if (item.Quantity <= 0)
                    strQty = "0";

               Console.WriteLine($"SecurityCode: {item.SecurityCode.SecurityCode}, Quantity: {strQty}");
            }

            Console.ReadLine();
        }
    }
}