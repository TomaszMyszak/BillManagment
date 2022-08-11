using R_III_WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Implemenatations
{
    public class BillsService : IBillsService
    {
        private readonly RIIIEntities context;
        public BillsService(RIIIEntities context)
        {
            this.context = context;
        }

        public bool Add(Bill bill)
        {
            try
            {
                context.Bill.Add(bill);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Edit(Bill bill)
        {
            var billdb = context.Bill.Where(b => b.Id == bill.Id).SingleOrDefault();

            if (billdb == null)
            {
                return false;
            }

            try
            {
                billdb.BillName = bill.BillName;
                billdb.April = bill.April;
                billdb.August = bill.August;
                billdb.BillsYear = bill.BillsYear;
                billdb.December = bill.December;
                billdb.February = bill.February;
                billdb.January = bill.January;
                billdb.July = bill.July;
                billdb.June = bill.June;
                billdb.March = bill.March;
                billdb.May = bill.May;
                billdb.November = bill.November;
                billdb.October = bill.October;
                billdb.September = bill.September;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Bill> GetBillsForUser(string userName)
        {
            var userId = context.Users.Where(u => u.UserName == userName).FirstOrDefault().Id;
            return context.Bill.Where(b => b.Users.UserName == userName);
        }
    }
}
