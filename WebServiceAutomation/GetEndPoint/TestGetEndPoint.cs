using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.GetEndPoint
{
    [TestClass]
    public class TestGetClass
    {
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string secureGetUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";
        private string delayget = "http://localhost:8080/laptop-bag/webapi/delay/all";

        [TestMethod]
        public void TestGetAllEndPoint()
        {
            //step 1
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync(getUrl);
            //close connection

            httpClient.Dispose();

        }

        [TestMethod]
        public void TestGetAllEndPointWithUri()
        {
            //step 1
            HttpClient httpClient = new HttpClient();
            Uri getUri =new Uri(getUrl);
            Task<HttpResponseMessage> httpResponse= httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
           
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);

            //response code
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            String data = responseData.Result;
            Console.WriteLine(data);
 
            httpClient.Dispose();


        }


        [TestMethod]
        public void TestGetAllEndPointWithInvalidUri()
        {
            //step 1
            HttpClient httpClient = new HttpClient();
            Uri getUri = new Uri(getUrl + "/random");
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;

            //static code
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);
            httpClient.Dispose();


        
        }

        [TestMethod]
        public void TestGetAllEndPointInJsonFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/json");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());


            //status code
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);

            //response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            String data = responseData.Result;
            Console.WriteLine(data);

            //close connction
            httpClient.Dispose();

        }

        [TestMethod]
        public void TestGetAllEndPointInXMLFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/xml");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());


            //status code
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);

            //response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            String data = responseData.Result;
            Console.WriteLine(data);

            //close connction
            httpClient.Dispose();

        }

        [TestMethod]
        public void TestGetAllEndPointInXMLFormatUsingAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue jsonHedader = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Accept.Add(jsonHedader);
            //requestHeaders.Add("Accept", "application/xml");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());


            //status code
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);

            //response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            String data = responseData.Result;
            Console.WriteLine(data);

            //close connction
            httpClient.Dispose();

        }


        [TestMethod]
        public void  TestGetPointUsingSendAsync()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(getUrl);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse =httpClient.SendAsync(httpRequestMessage);

            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());


            //status code
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status code " + statusCode);
            Console.WriteLine("Status code " + (int)statusCode);

            //response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            String data = responseData.Result;
            Console.WriteLine(data);

            //close connction
            httpClient.Dispose();

        }
        [TestMethod]
        public void TestUsingStatement()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using(HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());


                        //status code
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Status code " + statusCode);
                        //Console.WriteLine("Status code " + (int)statusCode);

                        //response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        String data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
                        Console.WriteLine(jsonRootObject[0].ToString());
                    }


                }

            }
        }

        [TestMethod]
        public void TestDeserilizationOfJsonResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());


                        //status code
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Status code " + statusCode);
                        //Console.WriteLine("Status code " + (int)statusCode);

                        //response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        String data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
                       Console.WriteLine(jsonRootObject[0].ToString());
                    }


                }

            }
        }
        [TestMethod]
        public void TestDeserilizationOfXmlResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/xml");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());


                        //status code
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        //Console.WriteLine("Status code " + statusCode);
                        //Console.WriteLine("Status code " + (int)statusCode);

                        //response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        String data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        //step 1
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));

                        //step 2
                        TextReader textReader = new StringReader(restResponse.ResponseContent);

                        //step 3
                        LaptopDetailss xmlData = (LaptopDetailss)xmlSerializer.Deserialize(textReader);
                        Console.WriteLine(xmlData.ToString());

                        //1st checkpoint (assertion) for statu code
                        Assert.AreEqual(200, restResponse.StatusCode);


                        //2nd check point (assertion) for response data
                        Assert.IsNotNull(restResponse.ResponseContent);

                        //3rd assertion
                        Assert.IsTrue(xmlData.Laptop[0].Features.Feature.Contains("Windows 10 Home 64-bit English"), "ITem no found");

                        //4th
                        Assert.AreEqual("Alienware", xmlData.Laptop[0].BrandName);

                    }


                }

            }

        }

        [TestMethod]
        public void GetUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            // List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            // Console.WriteLine(jsonRootObject[0].ToString());

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);

            Console.WriteLine(jsonData.ToString());

            


        }

        [TestMethod]
        public void TestSecureGetEndPoint()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            //httpHeader.Add("Authorization", "Basic YWRtaW46d2VsY29tZQ==");

            string authHeader = Base64StringConverter.GetBase64String("admin", "welcome");
            authHeader = "Basic " + authHeader;

            httpHeader.Add("Authorization", authHeader);

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            // List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            // Console.WriteLine(jsonRootObject[0].ToString());

            Assert.AreEqual(200, restResponse.StatusCode);

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);

            Console.WriteLine(jsonData.ToString());
        }

        [TestMethod]
        public void TestGetEndPoint_Sync()
        {   //1
            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);
            //2
            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);
            //3
            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);
            //statement 4
            HttpClientHelper.PerformGetRequest("http://localhost:8080/laptop-bag/webapi/delay/all", null);

        }
        [TestMethod]
        public void TestGetEndPoint_Async()
        {
            Task t1 = new Task(GetEndPoint());
            t1.Start();
            Task t2 = new Task(GetEndPoint());
            t2.Start();
            Task t3 = new Task(GetEndPoint());
            t3.Start();
            Task t4 = new Task(GetEndPoint());
            t4.Start();
            
            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait(); 
        }

        private Action GetEndPoint()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };
        
            return new Action(() =>
            {
               RestResponse restResponse =  HttpClientHelper.PerformGetRequest(delayget, headers);
                Assert.AreEqual(200, restResponse.StatusCode);
            });
        }

        private Action GetEndPoint_Fail()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            return new Action(() =>
            {
                RestResponse restResponse = HttpClientHelper.PerformGetRequest(delayget, headers);
                Assert.AreEqual(201, restResponse.StatusCode);
            });
        }
    }
}
