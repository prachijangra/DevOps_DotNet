using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookReadingEvent.Web.Models;
using BookReadingEvent.ProductDomain.AppServices;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using Microsoft.AspNetCore.Http;
using BookReadingEvent.ProductDomain.AppServices.Factory;
using BookReadingEvent.ProductDomain.AppServices.Facade;

namespace BookReadingEvent.Web.Controllers
{
    public class HomeController : Controller
    {
        /* private readonly ILogger<HomeController> _logger;
         private readonly ICreateEventService _eventServices;
         public HomeController(ICreateEventService eventser)
         {
             _eventServices = eventser;
         }*/


        private readonly FacadeFactory _facadeFactory;


    public HomeController(FacadeFactory facadeFactory)
    {
        _facadeFactory = facadeFactory;
    }


    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("EmailId") != null)
        {
            return RedirectToAction("Index", "Event");
        }
        ViewBag.UpcomingEvents = GetUpComingEvents();
        ViewBag.PastEvents = GetPastEvents();
        var data = GetAllEvents();
        return View(data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public List<Event> GetAllEvents()
    {
        List<Event> result = new List<Event>();
            //List<CreateEventDTO> store = _eventServices.GetAllPublicEvent();
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
            showEvent.EventId = x.EventId;
            result.Add(showEvent);
        }
        return result;
    }
    public List<Event> GetUpComingEvents()
    {
        List<Event> result = new List<Event>();
            //List<CreateEventDTO> store = _eventServices.GetUpcomingEvents();
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
            // List<CreateEventDTO> store = _eventServices.GetPastEvents();

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
