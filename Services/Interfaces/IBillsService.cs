using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Interfaces
{
    public interface IBillsService
    {
        IEnumerable<Bill> GetBillsForUser(string userName);
        bool Add(Bill bill);
        bool Edit(Bill bill);
    }
}
