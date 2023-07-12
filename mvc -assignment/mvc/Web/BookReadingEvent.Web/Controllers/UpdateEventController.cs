using BookReadingEvent.ProductDomain.AppServices;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BookReadingEvent.Web.Infrastructure;
using BookReadingEvent.ProductDomain.AppServices.Factory;
using BookReadingEvent.ProductDomain.AppServices.Facade;

namespace BookReadingEvent.Web.Controllers
{
    [CustomAuthFilter]
    public class UpdateEventController : Controller
    {
        // private readonly ICreateEventService _userEventService;


        // userservice is called dependency is injected 
        /* public UpdateEventController(ICreateEventService usereventService)
         {
             _userEventService = usereventService;
         } */

        private readonly FacadeFactory _facadeFactory;
        


        public UpdateEventController(FacadeFactory facadeFactory)
        {
            _facadeFactory = facadeFactory;
        }
        public IActionResult Index(int id)
        {
            ViewBag.emailId = HttpContext.Session.GetString("EmailId");
            HttpContext.Session.SetString("EventId", id.ToString());
            return View();
        }

        [HttpPost]
        public IActionResult Index(Event EventDetails)
        {
            CreateEventDTO newEvent = new CreateEventDTO();
            newEvent.Date = EventDetails.Date; 
            newEvent.Description = EventDetails.Description;
            newEvent.Duration = EventDetails.Duration;
            newEvent.Location = EventDetails.Location; 
            newEvent.StartTime = EventDetails.StartTime;
            newEvent.Title = EventDetails.Title; 
            newEvent.Type = EventDetails.Type;
            newEvent.EventId = int.Parse(HttpContext.Session.GetString("EventId"));
            EventFacade _eventFacade = (EventFacade)_facadeFactory.GetFacade("IEventFacade");
            _eventFacade.UpdateEvent(newEvent);
            // _userEventService.UpdateEvent(newEvent);
            return RedirectToAction("Index","MyEvent");
        }
    }
}
