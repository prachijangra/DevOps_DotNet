using BookReadingEvent.Core.AppServices;
using BookReadingEvent.Core.ValueObjects;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.ProductDomain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookReadingEvent.ProductDomain.AppServices
{
    public class UserLoginService : AppService, IUserLoginService
    {
        private readonly IUserRepository userRepositry = null;
        public UserLoginService(IUserRepository userrepo)
        {
            userRepositry = userrepo;
        }
        public bool AddUser(LoginDTO item)
        {
            bool result = userRepositry.LoginUser(item);
            return result;
        }
    }
}
