using System.Collections.Generic;
using Test.Model;

namespace Test.DataStore
{
    /// <summary>
    /// Simulates the database - In memory database
    /// </summary>
    public class DataStore
    {
        //Singleton DataStore instance
        private static readonly DataStore dataStore = new DataStore();
        private DataStore()
        {
            TradeSecurity = new List<TradeSecurity>();
            Transaction = new List<Transaction>();
            Position = new List<Position>();
        }

        public static DataStore GetInstance()
        {
            return dataStore;
        }

        public IList<TradeSecurity> TradeSecurity { get; set; }        

        public IList<Transaction> Transaction { get; set; }

        public IList<Position> Position { get; set; }        
    }
}