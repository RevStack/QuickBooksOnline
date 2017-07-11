using System;
using RevStack.Payment.Model;
using RevStack.Payment.Service;
using RevStack.QuickBooksOnline.Repository;
using System.Collections.Generic;

namespace RevStack.QuickBooksOnline.Service
{
    public class QuickBooksOnlinePaymentService : IPaymentService
    {
        private readonly QuickBooksOnlinePaymentRepository _repository;

        public QuickBooksOnlinePaymentService(QuickBooksOnlinePaymentRepository repository)
        {
            _repository = repository;
        }

        public T Authorize<T>(IAuthorize authorize) where T : IPayment
        {
            return _repository.Authorize<T>(authorize);
        }

        public T Capture<T>(ICapture capture) where T : IPayment
        {
            return _repository.Capture<T>(capture);
        }

        public T Charge<T>(ICharge charge) where T : IPayment
        {
            return _repository.Charge<T>(charge);
        }

        public T Credit<T>(ICredit credit) where T : IPayment
        {
            return _repository.Credit<T>(credit);
        }

        public T Get<T>() where T : IEnumerable<IPayment>
        {
            return _repository.Get<T>();
        }

        public T GetById<T>(ITransactionDetails transactionDetails) where T : IPayment
        {
            return _repository.GetById<T>(transactionDetails);
        }

        public T Void<T>(IVoid @void) where T : IPayment
        {
            return _repository.Void<T>(@void);
        }
    }
}
