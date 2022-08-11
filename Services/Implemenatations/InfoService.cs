using R_III_WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Implemenatations
{
    public class InfoService : IInfoService
    {
        private readonly RIIIEntities context;
        public InfoService(RIIIEntities context)
        {
            this.context = context;
        }

        public bool AddInformation(Information information)
        {
            try
            {
                context.Information.Add(information);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }   

        public IEnumerable<Information> GetAllForUser(string userName)
        {
            return context.Information.Where(i => i.Users.UserName == userName);
        }

        public string GetInformationInStringByUser(string userName)
        {
            var informations = GetAllForUser(userName);
            string result = "";

            foreach (Information info in informations)
            {
                result = $"{result}{info.InformationName}{Environment.NewLine}";
                result = $"{result}{info.Content}{Environment.NewLine}{Environment.NewLine}";
            }
            return result;
        }
    }
}
