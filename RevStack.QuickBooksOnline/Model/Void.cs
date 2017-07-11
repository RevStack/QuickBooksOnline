using RevStack.Payment.Model;

namespace RevStack.QuickBooksOnline.Model
{
    public class Void : IVoid
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Desciption { get; set; }
    }
}
