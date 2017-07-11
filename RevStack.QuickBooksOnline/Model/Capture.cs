using RevStack.Payment.Model;

namespace RevStack.QuickBooksOnline.Model
{
    public class Capture : ICapture
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}
