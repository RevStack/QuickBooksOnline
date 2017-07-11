using RevStack.Payment.Model;
using RevStack.Payment.Repository;
using RevStack.QuickBooksOnline.Context;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RevStack.QuickBooksOnline.Repository
{
    public class QuickBooksOnlinePaymentRepository : IPaymentRepository
    {
        private readonly QuickBooksOnlineContext _context;

        public QuickBooksOnlinePaymentRepository(QuickBooksOnlineContext context)
        {
            _context = context;
        }

        public T Authorize<T>(IAuthorize authorize) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges";

            var payment = new Model.Gateway.Payment();
            payment.Amount = authorize.Amount;
            payment.Currency = _context.Currency;

            var address = new Model.Gateway.Address();
            address.City = authorize.Customer.City;
            address.Country = authorize.Customer.Country;
            address.PostalCode = authorize.Customer.Zipcode;
            address.Region = authorize.Customer.StateOrProvince;
            address.StreetAddress = authorize.Customer.Address;

            var creditCard = new Model.Gateway.CreditCard();
            creditCard.Name = authorize.Customer.FirstName + " " + authorize.Customer.LastName;
            creditCard.Address = address;
            creditCard.Number = authorize.CreditCard.CardNumber;
            creditCard.ExpMonth = authorize.CreditCard.ExpirationMonth;
            creditCard.ExpYear = authorize.CreditCard.ExpirationYear;
            creditCard.CVC = authorize.CreditCard.CVV;

            payment.Card = creditCard;
            payment.Capture = "false";

            var body = JsonConvert.SerializeObject(payment);

            var response = _context.SendRequest(url, "POST", body, new Dictionary<string, string>(), null);

            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T Capture<T>(ICapture capture) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges/" + capture.Id + "/capture";
            var body = new JObject();
            body["amount"] = capture.Amount;
            var response = _context.SendRequest(url, "POST", body.ToString(), new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T Charge<T>(ICharge charge) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges";

            var payment = new Model.Gateway.Payment();
            payment.Amount = charge.Amount;
            payment.Currency = _context.Currency;

            var address = new Model.Gateway.Address();
            address.City = charge.Customer.City;
            address.Country = charge.Customer.Country;
            address.PostalCode = charge.Customer.Zipcode;
            address.Region = charge.Customer.StateOrProvince;
            address.StreetAddress = charge.Customer.Address;

            var creditCard = new Model.Gateway.CreditCard();
            creditCard.Name = charge.Customer.FirstName + " " + charge.Customer.LastName;
            creditCard.Address = address;
            creditCard.Number = charge.CreditCard.CardNumber;
            creditCard.ExpMonth = charge.CreditCard.ExpirationMonth;
            creditCard.ExpYear = charge.CreditCard.ExpirationYear;
            creditCard.CVC = charge.CreditCard.CVV;
            payment.Card = creditCard;

            var body = JsonConvert.SerializeObject(payment);
            var response = _context.SendRequest(url, "POST", body, new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T Credit<T>(ICredit credit) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges/" + credit.Id + "/refunds";
            var refundCredit = (Model.Credit)credit;
            var refund = new Model.Gateway.Refund();
            refund.Amount = refundCredit.Amount;

            var context = new Model.Gateway.Context();
            context.Tax = "0";
            context.Recurring = "false";

            refund.Context = context;
            refund.Description = refundCredit.Desciption;
            var body = JsonConvert.SerializeObject(refund);
            var response = _context.SendRequest(url, "POST", body.ToString(), new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T Void<T>(IVoid @void) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges/" + @void.Id + "/refunds";
            var voidCredit = (Model.Void)@void;
            var refund = new Model.Gateway.Refund();
            refund.Amount = voidCredit.Amount;

            var context = new Model.Gateway.Context();
            context.Tax = "0";
            context.Recurring = "false";

            refund.Context = context;
            refund.Description = voidCredit.Desciption;
            var body = JsonConvert.SerializeObject(refund);
            var response = _context.SendRequest(url, "POST", body.ToString(), new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T Get<T>() where T : IEnumerable<IPayment>
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges/";
            var response = _context.SendRequest(url, "GET", "", new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }

        public T GetById<T>(ITransactionDetails transactionDetails) where T : IPayment
        {
            var url = _context.Url + "/quickbooks/v4/payments/charges/" + transactionDetails.Id;
            var response = _context.SendRequest(url, "GET", "", new Dictionary<string, string>(), null);
            return JsonConvert.DeserializeObject<T>(response.Body.ToString());
        }
    }
}
  