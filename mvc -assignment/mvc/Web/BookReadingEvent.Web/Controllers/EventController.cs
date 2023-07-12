using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BookReadingEvent.ProductDomain.AppServices;
using BookReadingEvent.Web.Models;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.Web.Infrastructure;
using BookReadingEvent.ProductDomain.AppServices.Factory;
using BookReadingEvent.ProductDomain.AppServices.Facade;
using AutoMapper;

namespace BookReadingEvent.Web.Controllers
{
    [CustomAuthFilter(Roles = "user,admin")]
    public class EventController : Controller
    {

        // private readonly ICreateEventService _eventServices;
        /* public EventController(ICreateEventService eventser)
         {
             _eventServices = eventser;
         }*/
        private readonly IMapper _mapper;

        

        private readonly FacadeFactory _facadeFactory;
        public EventController(FacadeFactory facadeFactory, IMapper Mapper)
        {
           
            _facadeFactory = facadeFactory;
            _mapper = Mapper;
            
        }

        public IActionResult Index()
        {
            ViewBag.emailId = HttpContext.Session.GetString("EmailId");
            ViewBag.UpcomingEvents = GetUpComingEvents();
            ViewBag.PastEvents = GetPastEvents();
            var data = GetAllEvents();
            return View(data);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        public List<Event> GetAllEvents()
        {
            List<Event> result = new List<Event>();
            //ist<CreateEventDTO> store = _eventServices.GetAllPublicEvent();

            EventFacade _eventFacade = (EventFacade)_facadeFactory.GetFacade("IEventFacade");
            List<CreateEventDTO> store = _eventFacade.GetAllPublicEvent();


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
                result.Add(showEvent);
            }
            return result;
        }
        public List<Event> GetUpComingEvents()
        {
            List<Event> result = new List<Event>();
            // List<CreateEventDTO> store = _eventServices.GetUpcomingEvents();

            EventFacade _eventFacade = (EventFacade)_facadeFactory.GetFacade("IEventFacade");
            List<CreateEventDTO> store = _eventFacade.GetUpcomingEvents();


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
        public List<Event> GetPastEvents()
        {
            List<Event> result = new List<Event>();
            //List<CreateEventDTO> store = _eventServices.GetPastEvents();
            EventFacade _eventFacade = (EventFacade)_facadeFactory.GetFacade("IEventFacade");
            List<CreateEventDTO> store = _eventFacade.GetPastEvents();
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
