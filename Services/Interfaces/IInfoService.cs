using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Interfaces
{
    public interface IInfoService
    {
        IEnumerable<Information> GetAllForUser(string userName);
        bool AddInformation(Information infomation);
        string GetInformationInStringByUser(string userName);
    }
}
