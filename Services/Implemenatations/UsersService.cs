using R_III_WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_III_WPF.Services.Implemenatations
{
    public class UsersService : IUsersService
    {
        private readonly RIIIEntities context;
        public UsersService(RIIIEntities context)
        {
            this.context = context;
        }

        public bool EditUser(Users user)
        {
            var userdb = context.Users.Where(u => u.Id == user.Id).SingleOrDefault();

            try
            {
                userdb.PhoneNumber = user.PhoneNumber;
                userdb.Surname = user.Surname;
                userdb.UserName = user.UserName;
                userdb.Pasword = user.Pasword;
                userdb.Email = user.Email;
                userdb.IsPaid = user.IsPaid;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Users> GetAll()
        {
            return context.Users.ToList();
        }

        public bool Login(Users user)
        {
            var a = context.Users.ToList();
            if (context.Users.Where(u => u.UserName == user.UserName && u.Pasword == user.Pasword).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(Users user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public int GetUserIdByName(string userName)
        {
            return context.Users.Where(u => u.UserName == userName).FirstOrDefault().Id;
        }
    }
}
