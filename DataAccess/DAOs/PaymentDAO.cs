using DataAccess.Context;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class PaymentDAO
    {
        private ApplicationDbContext _context;
        private static PaymentDAO _instance;
        private static readonly object _instanceLock = new object();

        public static PaymentDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        throw new InvalidOperationException("PaymentDAO has not been initialized. Call Initialize method first.");
                    }
                    return _instance;
                }
            }
        }

        public PaymentDAO(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public static void Initialize(ApplicationDbContext dbContext)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new PaymentDAO(dbContext);
                }
            }
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            try
            {
                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();

                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePaymentStatusAsync(string content, string message, string code)
        {

            try
            {
                Payment paymentExist = await _context.Payments.FirstOrDefaultAsync(payment => payment.PaymentContent.Equals(content));

                paymentExist.PaymentMessage = message;
                paymentExist.PaymentStatus = code;

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
