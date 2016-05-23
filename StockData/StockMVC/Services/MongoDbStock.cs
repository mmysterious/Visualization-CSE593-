using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using StockMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Services
{
    public class MongoDbStock : IStockData
    {
        public async Task<IEnumerable<IDictionary<string, string>>> Get(string name, DateTime from, DateTime to)
        {
            var client = new MongoClient("mongodb://yhstock:yhstock@ds030719.mlab.com:30719/yhstock");
            var db = client.GetDatabase("yhstock");
            var collection = db.GetCollection<StockRecord>("yhstock");
            var unixStart = new DateTime(1970, 1, 1);
            var begin = (int)(from - unixStart).TotalDays;
            var end = (int)(to - unixStart).TotalDays;

            var stockList = await (await collection
                .FindAsync(b => b.StockName == name
                && b.Date >= begin && b.Date <= end))
                .ToListAsync();

            if (stockList.Count() == end - begin + 1)
            {
                var result = new List<IDictionary<string, string>>();
                foreach (var stockRecord in stockList)
                {
                    var jsonStr = stockRecord.HistoryRecord.ToJson();
                    var recordDict = JsonConvert.DeserializeObject
                        <Dictionary<string, string>>(jsonStr);
                    recordDict["Date"] = stockRecord.Date.ToString();
                    result.Add(recordDict);
                }
                return result;
            }
            else
            {
                var result = await (new FromYahooStock()).Get(name, from, to);
                foreach (var recordDict in result)
                {
                    var jsonStr = recordDict.ToJson();
                    var stockHistoryRecord = JsonConvert.DeserializeObject
                        <StockHistoryRecord>(jsonStr);
                    var stockRecord = new StockRecord
                    {
                        StockName = name,
                        Date = int.Parse(recordDict["Date"]),
                        HistoryRecord = stockHistoryRecord,
                        Id = ObjectId.GenerateNewId()
                    };
                    //stockRecord.Id = new ObjectId(name + stockRecord.Date.ToString());

                    if (null == stockList.Find(b => b.StockName == name && b.Date == stockRecord.Date))
                    {
                        collection.InsertOne(stockRecord);
                    }
                    else
                    {
                        //collection.FindOneAndReplace(b => b.StockName == name
                        //&& b.Date == stockRecord.Date, stockRecord);
                    }
                }
                return result;
            }
        }
    }
}
