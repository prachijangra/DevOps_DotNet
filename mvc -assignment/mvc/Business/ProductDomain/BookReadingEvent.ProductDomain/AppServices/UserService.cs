using BookReadingEvent.Core.AppServices;
using BookReadingEvent.Core.ValueObjects;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.ProductDomain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookReadingEvent.ProductDomain.AppServices
{
    public class UserService : AppService, IUserService
    {
        private readonly IUserRepository userRepositry = null;
        public UserService(IUserRepository userrepo)
        {
            userRepositry = userrepo;
        }
        public bool AddUser(SignUpDTO item)
        {
           string result = userRepositry.AddUser(item);
            return true;
        }
        public string GetInvitedEventId(string Email)
        {
            return userRepositry.GetInvitedEventsString(Email);
        }
    }
}
