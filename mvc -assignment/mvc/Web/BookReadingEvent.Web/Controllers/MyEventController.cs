using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BookReadingEvent.ProductDomain.AppServices;
using BookReadingEvent.ProductDomain.AppServices.Factory;
using BookReadingEvent.ProductDomain.AppServices.Facade;
using BookReadingEvent.Web.Infrastructure;

namespace BookReadingEvent.Web.Controllers
{
    [CustomAuthFilter]
    public class MyEventController : Controller
    {
        /*  private readonly ICreateEventService _eventServices;
          public MyEventController(ICreateEventService eventser)
          {
              _eventServices = eventser;
          }*/
        private readonly FacadeFactory _facadeFactory;
        
        public MyEventController(FacadeFactory facadeFactory)
        {
            _facadeFactory = facadeFactory;
        }
        public IActionResult Index()
        {
            ViewBag.emailId = HttpContext.Session.GetString("EmailId");
            ViewBag.MyEvents = MyEvents();
            string email = HttpContext.Session.GetString("EmailId");
            if (email != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public List<Event> MyEvents()
        {
            List<Event> result = new List<Event>();
            var email = HttpContext.Session.GetString("EmailId");
            //List<CreateEventDTO> store = _eventServices.MyEvents(email);
            EventFacade _eventFacade = (EventFacade)_facadeFactory.GetFacade("IEventFacade");
            List<CreateEventDTO> store = _eventFacade.MyEvents(email);
            foreach (var x in store)
            {
                Event showEvent = new Event();
                showEvent.Date = x.Date;
                showEvent.Description = x.Description;
                showEvent.Duration = x.Duration;
                showEvent.InviteByEmail = x.InviteByEmail;
                showEvent.Location = x.Location;
                showEvent.OtherDetails = x.OtherDetails;
                showEvent.StartTime = x.StartTime;
                showEvent.Title = x.Title;
                showEvent.Type = x.Type;
                showEvent.EventId = x.EventId;
                result.Add(showEvent);
            }
            return result;
        }
    }
}
