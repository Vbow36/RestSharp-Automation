using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("Smoke")]
        public void TestMethod1()
        {
            Console.WriteLine("test method one");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Console.WriteLine("Test method two");  //2. 3. 4
        }

       [TestInitialize]
        public void Setup() //1.4.2. 5, 3, 6
        {
            Console.WriteLine("This is setup");
        }

        [TestCleanup]   
        public void TearDown()
        {
            Console.WriteLine("This is Clean up"); //3. 6. 4. 7, 5
        }
        [ClassInitialize]
        public static void classSetup(TestContext testContext)  //1, 2
        {
            Console.WriteLine("Class set up");
        }

       [ClassCleanup]
       public static void ClassTearDown()
        {
            Console.WriteLine("Class tear down");// 8, 9
        }

        [AssemblyInitialize]
        public static void AssemblySetup(TestContext testContext)  //1st
        {
            Console.WriteLine("Assembly setup");

        }
        [AssemblyCleanup]
        public static void AssemblyTearDown()
        {
            Console.WriteLine(" Assembly tear down");  //10
        }


    }

}
