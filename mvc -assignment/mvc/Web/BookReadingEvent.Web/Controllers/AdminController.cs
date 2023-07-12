using BookReadingEvent.ProductDomain.AppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using AutoMapper;
using BookReadingEvent.Web.Models;
using Microsoft.AspNetCore.Http;
using BookReadingEvent.ProductDomain.AppServices.Factory;
using BookReadingEvent.ProductDomain.AppServices.Facade;
using BookReadingEvent.Web.Infrastructure;

namespace BookReadingEvent.Web.Controllers
{
    [CustomAuthFilter]
    public class AdminController : Controller
    {
       // private readonly ICreateEventService _userEventService;

        
        // userservice is called dependency is injected 
        /* public AdminController(ICreateEventService usereventService,IMapper mapper)
         {
             _mapper = mapper;
             _userEventService = usereventService;
         } */
        private readonly FacadeFactory _facadefactory;
        
        private readonly IMapper _mapper;
      
        public AdminController(FacadeFactory facadeFactory, IMapper mapper)
        {
            _facadefactory = facadeFactory;
            _mapper = mapper;
        }

        public IActionResult Index()

        {
            
            ViewBag.emailId = HttpContext.Session.GetString("EmailId");
            EventFacade _eventFacade = (EventFacade)_facadefactory.GetFacade("IEventFacade");

            List<CreateEventDTO> allEvent = _eventFacade.GetAllEvent();
            List<Event> allEventList= _mapper.Map<List<CreateEventDTO>, List<Event>>(allEvent);
            ViewBag.AdminEvent = allEventList;
            string email = HttpContext.Session.GetString("EmailId");
           
                return View();
            
          
        }
    }
}
