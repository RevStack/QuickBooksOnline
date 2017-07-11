using RevStack.Payment.Model;

namespace RevStack.QuickBooksOnline.Model
{
    public class CreditCard : ICreditCard
    {
        public string Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
    }
}
