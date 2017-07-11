using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevStack.QuickBooksOnline.Context;
using RevStack.QuickBooksOnline.Repository;
using RevStack.QuickBooksOnline.Service;
using RevStack.QuickBooksOnline.Model;
using RevStack.Payment;
using RevStack.Payment.Model;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PaymentTest
    {
        private readonly string consumerKey = "";
        private readonly string consumerSecret = "";
        private readonly string accessToken = "";
        private readonly string accessTokenSecret = "";
        private readonly string realmId = "";

        private readonly ServiceMode serviceMode;
        private readonly QuickBooksOnlineContext context;
        private readonly QuickBooksOnlinePaymentRepository repository;
        private readonly QuickBooksOnlinePaymentService service;

        public PaymentTest()
        {
            serviceMode = ServiceMode.Test;
            context = new QuickBooksOnlineContext(consumerKey, consumerSecret, accessToken, accessTokenSecret, "", serviceMode, "USD", realmId, "");
            repository = new QuickBooksOnlinePaymentRepository(context);
            service = new QuickBooksOnlinePaymentService(repository);
        }

        [TestMethod]
        public void Charge()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "Biggs";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<RevStack.QuickBooksOnline.Model.Gateway.Payment>(charge);
            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void AuthorizeAndCapture()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "McDougle";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            IAuthorize authorize = new Authorize();
            authorize.Customer = customer;
            authorize.Shipping = shipping;
            authorize.CreditCard = creditCard;
            authorize.Amount = 10;

            var response = service.Authorize<RevStack.QuickBooksOnline.Model.Gateway.Payment>(authorize);
            Assert.AreNotEqual(null, response);

            ICapture capture = new Capture();
            capture.Amount = 10;
            capture.Id = response.Id;

            response = service.Capture<RevStack.QuickBooksOnline.Model.Gateway.Payment>(capture);
            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void ChargeAndCredit()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "McDougle";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<RevStack.QuickBooksOnline.Model.Gateway.Payment>(charge);
            Assert.AreNotEqual(null, response);

            ICredit credit = new Credit();
            credit.Amount = 8;
            credit.Id = response.Id;

            response = service.Credit<RevStack.QuickBooksOnline.Model.Gateway.Payment>(credit);
            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void ChargeAndVoid()
        {
            ICustomer customer = new Customer();
            customer.Address = "555 Main St.";
            customer.City = "Charlotte";
            customer.Country = "US";
            customer.Email = "***@****.com";
            customer.FirstName = "Bob";
            customer.LastName = "McDougle";
            customer.Phone = "5555555555";
            customer.StateOrProvince = "NC";
            customer.Zipcode = "28202";

            ICreditCard creditCard = new CreditCard();
            creditCard.CardNumber = "4111111111111111";
            creditCard.CVV = "123";
            creditCard.ExpirationMonth = "02";
            creditCard.ExpirationYear = "2020";

            IShipping shipping = new Shipping();
            shipping.Address = "555 Main St.";
            shipping.City = "Charlotte";
            shipping.Country = "US";
            shipping.Email = "***@****.com";
            shipping.FirstName = "Bog";
            shipping.LastName = "Biggs";
            shipping.Phone = "5555555555";
            shipping.StateOrProvince = "NC";
            shipping.Zipcode = "28202";

            ICharge charge = new Charge();
            charge.Customer = customer;
            charge.Shipping = shipping;
            charge.CreditCard = creditCard;
            charge.Amount = 10;

            var response = service.Charge<RevStack.QuickBooksOnline.Model.Gateway.Payment>(charge);
            Assert.AreNotEqual(null, response);

            IVoid @void = new RevStack.QuickBooksOnline.Model.Void();
            @void.Amount = 10;
            @void.Id = response.Id;

            response = service.Void<RevStack.QuickBooksOnline.Model.Gateway.Payment>(@void);
            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void GetById()
        {
            var transaction = new Transaction();
            transaction.Id = "EG5EKQ0LKL84";

            var payment = service.GetById<RevStack.QuickBooksOnline.Model.Gateway.Payment>(transaction);
            Assert.AreNotEqual(null, payment);
        }

        [TestMethod]
        public void Get()
        {
            var transactions = service.Get<IEnumerable<RevStack.QuickBooksOnline.Model.Gateway.Payment>>();
            Assert.AreNotEqual(0, transactions.Count());
        }
    }
}
