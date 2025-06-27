using EComm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStates(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentID(int Id, string sessionId, string paymentIntentId);
    }
}
