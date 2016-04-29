using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic.FileIO;
using StockData.Models;
using System.Net;

namespace StockData
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FinanceDBContext db = new FinanceDBContext();
            string filepath = "FinancialData.csv";
            string path = Server.MapPath("~/Content/" + filepath);
            FetchData(path);
        }

        protected void FetchData(string filepath)
        {
            // string remoteUri = "http://real-chart.finance.yahoo.com/table.csv?s=YHOO&d=3&e=25&f=2016&g=d&a=3&b=12&c=1996&ignore=.csv";
            string remoteUri = "http://www.google.com/finance/historical?q=AAPL&startdate=Nov+1%2C+2011&enddate=Nov+30%2C+2011&num=30&output=csv";
            string fileName = Server.MapPath("~/Content/" + "/data_table_m.csv");
            string myStringWebResource = null;

            // Create a new WebClient instance.
            using (WebClient myWebClient = new WebClient())
            {
                myStringWebResource = remoteUri;
                // Download the Web resource and save it into the current filesystem folder.
                myWebClient.DownloadFile(myStringWebResource, fileName);
            }
            DataTable dt = new DataTable();
            bool isFirstRowHeader = true;
            string[] columns = new string[] { "" };


            using (TextFieldParser parser = new TextFieldParser(filepath))
            {
                parser.TrimWhiteSpace = true;
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                if (isFirstRowHeader)
                {
                    columns = parser.ReadFields();
                    foreach(string c in columns)
                    {
                        DataColumn finance = new DataColumn(c.Trim().ToLower(), Type.GetType("System.String"));
                        dt.Columns.Add(finance);
                    }
                }
                while (true)
                {
                    if (isFirstRowHeader == false)
                    {
                        string[] parts = parser.ReadFields();
                        if (parts == null)
                        {
                            break;
                        }
                        dt.Rows.Add(parts);
                    }
                    isFirstRowHeader = false;

                }
            }
        }
    }
}