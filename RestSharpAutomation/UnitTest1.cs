using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            /*
             * 1. craete client
             * 2 clreate rewuest
             * 3 send request using client
             * 4 capture response
             */

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest();



        }
    }
}
