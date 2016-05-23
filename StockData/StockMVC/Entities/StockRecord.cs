using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Entities
{
    public class StockRecord
    {
        public ObjectId Id { get; set; }
        public string StockName { get; set; }
        public int Date { get; set; }
        public StockHistoryRecord HistoryRecord { get; set; }
    }

    public class StockHistoryRecord
    {
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public int Volume { get; set; }
        public float Adj_Close { get; set; }
    }
}
