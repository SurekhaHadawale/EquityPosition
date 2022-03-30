The solution has four layers

1.Service Layer:

Test.TradeService => Client interfaces with the Service layer to process Transactions and get Equity Positions.

2.Business Layer:
Test.TradeProcessor => This contains the core logic to process Transactions from client.
a.ITransactionProcessor - The main abstraction to process transaction.
b.TransactionProcessor - Concrete implementation of ITransactionProcessor.
						 Has factory method to create specific type of Trade processor. This can be in future done via Dependency Injection.

c.ITradeProcessor - Abstraction to process on type of Trade. In future we can have different ways of handling insert, update & cancel actions.
d.BaseTradeProcessor - Abstract class to implememt common Trade processing logic.
e.InsertTradeProcessor - Encapsulates how to handle insert of trade as per current Rules.
f.UpdateTradeProcessor - Encapsulates how to handle update of trade as per current Rules.
g.CancelTradeProcessor - Encapsulates how to handle cancel of trade as per current Rules.

3.Model:
Test.Model represents the domain entities

4.DataStore:
Test.DataStore simulates the database - In memory database
