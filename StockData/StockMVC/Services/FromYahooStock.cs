using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockMVC.Services
{
    public class FromYahooStock : IStockData
    {
        public async Task<IDictionary<string, IEnumerable<IDictionary<string, string>>>> Get(string[] names, DateTime from, DateTime to)
        {
            var result = new Dictionary<string, IEnumerable<IDictionary<string, string>>>();
            foreach (var name in names)
            {
                result[name] = await Get(name, from, to);
            }
            return result;
        }

        public async Task<IEnumerable<IDictionary<string, string>>> Get(string name, DateTime from, DateTime to)
        {
            var allStocks = new Dictionary<string, IEnumerable<IDictionary<string, string>>>();

            var url = String.Format(
                "http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&ignore=.csv",
                name, from.Month - 1, from.Day, from.Year,
                to.Month - 1, to.Day, to.Year);

            using (var web = new HttpClient())
            {
                try
                {
                    var result = await web.GetStringAsync(url);
                    return Parse(result);
                }
                catch (HttpRequestException)
                {
                    throw new Exception(url);
                }
            }
        }

        IEnumerable<IDictionary<string, string>> Parse(string csvData)
        {
            var rows = csvData.Split('\n');
            var list = new List<Dictionary<string, string>>();
            if (rows.Length < 2)
                return list;
            else
            {
                var fields = rows[0].Split(',');
                foreach (var row in rows.Skip(1))
                {
                    if (string.IsNullOrEmpty(row)) continue;
                    var record = new Dictionary<string, string>();
                    var values = row.Split(',');
                    for (var i = 0; i < fields.Length; i++)
                    {
                        if (fields[i] == "Date")
                        {
                            var date = DateTime.ParseExact(values[i],
                                "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            var unixStart = new DateTime(1970, 1, 1);
                            record.Add(fields[i], ((int)(date - unixStart).TotalDays).ToString());
                        }
                        else
                        {
                            record.Add(fields[i].Replace(' ', '_'), values[i]);
                        }
                    }

                    list.Add(record);
                }
                return list;
            }
        }
    }
}
