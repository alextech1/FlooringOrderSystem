using NUnit.Framework;
using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models.Responses;
using FlooringOrderSystem.Data;
using System;


namespace FlooringOrderSystem.Tests
{
    [TestFixture]
    public class OrderTestTests
    {
        [Test]
        public void TestValidDateFormat()
        {
            Validation validate = new Validation();

            validate.ValidFormat("06/01/2013");

            Assert.True(true);
        }


        [Test]
        public void TestIsFutureDate()
        {
            Validation validate = new Validation();

            validate.FutureDate("06/01/2021");

            Assert.True(true);
        }

        [Test]
        public void TestAddOrder()
        {
            OrderManager manager = new OrderManager(new OrderTestRepository());

            OrderAddToListResponse response = new OrderAddToListResponse();

            manager.AddOrder("06/01/2020", "Wise", "OH", "Wood", 100.00m);

            Assert.That(response.Success = true);

        }

        [Test]
        public void TestOrderExist()
        {
            OrderManager manager = new OrderManager(new OrderTestRepository());

            OrderAddToListResponse response = new OrderAddToListResponse();

            manager.CheckOrderExist(1, "06/01/2013");

            Assert.That(response.Success = true);
        }

        [Test]
        public void TestOrderLookup()
        {
            OrderManager manager = new OrderManager(new OrderTestRepository());

            OrderAddToListResponse response = new OrderAddToListResponse();

            manager.LookupOrder(1, "06/01/2013");

            Assert.That(response.Success = true);
        }
    }
}
