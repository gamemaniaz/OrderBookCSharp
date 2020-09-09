using System;
using System.Text;
using Elasticsearch.Net;
using log4net;
using NUnit.Framework;
using OrderBook.Orders;

namespace OrderBookTests
{
    public class Tests
    {

        // [Test]
        // public void TestLoggingToLogstash()
        // {
        //     LogManager.GetLogger()
        // }
        
        // [Test]
        // public void TestElasticSearch()
        // {
        //     var settings = new ConnectionConfiguration(new Uri("http://localhost:9200"))
        //         .RequestTimeout(TimeSpan.FromMinutes(2));
        //     var lowlevelClient = new ElasticLowLevelClient(settings);
        //
        //     var order = new Order
        //     {
        //         Side = 0,
        //         Size = 100,
        //         Price = 40,
        //         Trader = "louis"
        //     };
        //     
        //     var indexResponse = lowlevelClient.Index<BytesResponse>("order", "1", PostData.Serializable(order));
        //     
        //     byte[] responseBytes = indexResponse.Body;
        //
        //     Console.WriteLine(Encoding.UTF8.GetString(responseBytes));
        // }
    }
}