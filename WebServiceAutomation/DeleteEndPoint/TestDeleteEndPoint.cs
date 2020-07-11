using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.DeleteEndPoint
{
    [TestClass]
    public class TestDeleteEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string deleteEndPoint = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private string secureDeleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private RestResponse restResponse;
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();

        [TestMethod]
        public void TestDelete()
        {
            /*
             * 1. using teh post i will add a record in applicatn
             * 2. call the delete end point   200 ok
             * 3. call the delete endpoint  > 404 
             * */

            int id = random.Next(1000);
            AddRecord(id);

            using(HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.DeleteAsync(deleteEndPoint + id);
                HttpStatusCode httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(200, (int)httpStatusCode);

               httpResponseMessage = httpClient.DeleteAsync(deleteEndPoint + id);
                httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(404, (int)httpStatusCode);

            }
        }

        public void AddRecord(int id)
        {
           
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };
            restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, headers);

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType);

            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            //Console.WriteLine(xmlData.ToString());
        }

        [TestMethod]
       public void TestDeleteUsingHelperClass()
        {
            int id = random.Next(1000);

            AddRecord(id);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteEndPoint + id);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteEndPoint + id);
            Assert.AreEqual(404, restResponse.StatusCode);

        }

        [TestMethod]
        public void TestSecureDeleteUsingHelperClass()
        {
            int id = random.Next(1000);

            AddRecord(id);

            string auth = Base64StringConverter.GetBase64String("admin", "welcome");
            auth = "Basic" + auth;

            Dictionary<string, string> headers = new Dictionary<string, string>()
           {
               {"Accept","application/xml" },
               {"Authorization", auth }
           };

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, headers);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, headers);
            Assert.AreEqual(404, restResponse.StatusCode);
        }
    }

   




    
            
    
}
