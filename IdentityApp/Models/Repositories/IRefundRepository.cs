using System.Collections.Generic;

namespace IdentityApp.Models.Repositories
{
    public interface IRefundRepository
    {
        bool Create(Refund refund);
        IEnumerable<Refund> GetAllRefunds();
    }
}
