using AutoMapper;
using BookReadingEvent.Core.Data.DataAccess;
using BookReadingEvent.ProductDomain.AppServices.DTOs;
using BookReadingEvent.ProductDomain.Data.DBContext;
using BookReadingEvent.ProductDomain.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookReadingEvent.ProductDomain.Repository
{
    public class CreateEventRepositry : ICreateEventRepositry
    {
        private readonly ILogger<CreateEventRepositry> _Ilogger;
        public ProductDomainDbContext _obj;
      
        private readonly IMapper _mapper;
        public CreateEventRepositry(ProductDomainDbContext context,IMapper mapper, ILogger<CreateEventRepositry> ilogger)
        {
            _obj = context;
            _mapper = mapper;
            _Ilogger = ilogger;
        }
       
        public void AddEvent(CreateEventDTO eventDetails)
        {
            EventCreate newEvent = new EventCreate();
            newEvent=_mapper.Map<CreateEventDTO,EventCreate>(eventDetails);
            _obj.CreateEvent.Add(newEvent);
            
            _obj.SaveChanges();
            _Ilogger.LogInformation($"Event is created and saved"); 
        }

        public List<CreateEventDTO> UpcomingEvents()
        {
            List<CreateEventDTO> result = _obj.CreateEvent.Select(x => new CreateEventDTO()
            {
                EventId = x.Id,
                Title = x.Title,
                Duration = x.Duration,
                Description = x.Description,
                Date = x.Date,
                Location = x.Location,
                StartTime = x.StartTime,
                Type = x.Type,
                OtherDetails = x.OtherDetails,
                InviteByEmail = x.InviteByEmail,
            }).Where(x => x.Type == "public").ToList();
            _Ilogger.LogInformation("Showing public events");
            return result;
        }
        public List<CreateEventDTO> MyEvents(string email)
        {
            List<CreateEventDTO> result = _obj.CreateEvent.Select(x => new CreateEventDTO()
            {
                EventId = x.Id,
                Title = x.Title,
                Duration = x.Duration,
                Description = x.Description,
                Date = x.Date,
                Location = x.Location,
                StartTime = x.StartTime,
                Type = x.Type,
                OtherDetails = x.OtherDetails,
                InviteByEmail = x.InviteByEmail,
                Creator = x.Creator
            }).ToList().Where(x => x.Creator.Equals(email)).ToList();
            _Ilogger.LogInformation("getting my events");
            return result;
        }
        public CreateEventDTO GetEventById(int id)
        {
            CreateEventDTO eventDetails = new CreateEventDTO();
            /*
                       List<CreateEventDTO> result = _obj.CreateEvent.Select(x => new CreateEventDTO()
                        {
                            EventId = x.Id,
                            Title = x.Title,
                            Duration = x.Duration,
                            Description = x.Description,
                            Date = x.Date,
                            Location = x.Location,
                            StartTime = x.StartTime,
                            Type = x.Type,
                            OtherDetails = x.OtherDetails,
                            InviteByEmail = x.InviteByEmail,
                            Creator = x.Creator
                        }).ToList();*/
            List<EventCreate> store = _obj.CreateEvent.ToList();
            List<CreateEventDTO> result = _mapper.Map<List<EventCreate>, List<CreateEventDTO>>(store);
            eventDetails = result.Where(x => x.EventId.Equals(id)).FirstOrDefault();
            return eventDetails;
        }
        public List<CreateEventDTO> GetAllEvent()
        {
            List<EventCreate> store = _obj.CreateEvent.ToList();
            List<CreateEventDTO> result = _mapper.Map<List<EventCreate>, List<CreateEventDTO>>(store);
            return result;
        }
        public void TagInvitedEventToUser(List<string> AllMailID)
        {
            for (int i = 0; i < AllMailID.Count; i++)
            {
                var cnt = _obj.CreateEvent.Max(x => x.Id);
                var checkMail = AllMailID[i];
                var obj = _obj.UserAccounts.Where(x => x.EmailID == checkMail).FirstOrDefault();
                if (obj != null)
                {
                    obj.InvitedEvent += cnt + ",";
                }

            }
            _obj.SaveChanges();
            return;
        }
        public void DeleteByEventId(int id)
        {
            var row = _obj.CreateEvent.Where(x => x.Id == id).FirstOrDefault();
            _obj.CreateEvent.Remove(row);
            _obj.SaveChanges();
        }
        public void UpdateEvent(CreateEventDTO eventDetails)
        {
            EventCreate oldEvent = _obj.CreateEvent.Find(eventDetails.EventId);
            oldEvent.Date = eventDetails.Date;
            oldEvent.Description = eventDetails.Description;
            oldEvent.Duration = eventDetails.Duration;
            oldEvent.Location = eventDetails.Location;
            oldEvent.StartTime = eventDetails.StartTime;
            oldEvent.Type = eventDetails.Type;
            oldEvent.Title = eventDetails.Title;
            _obj.SaveChanges();
        }
        public void AddComment(CommentDTO commentDetails)
        {
            var eventcreator = _obj.CreateEvent.Find(commentDetails.EventID);
            string _creatorId = eventcreator.Creator;
            Comment store = _mapper.Map<CommentDTO, Comment>(commentDetails);
            store.CreatorID = _creatorId;
            store.EventName = eventcreator.Title;
            _obj.Comments.Add(store);
            _obj.SaveChanges();
        }
        public List<CommentDTO> GetAllComments(int ID)
        {
            var result = _obj.Comments.Select(x => new CommentDTO()
            {
                EventID = x.EventID,
            
                Comment1 = x.Comment1,
            
                Email= x.Email
            }).Where(x => x.EventID == ID).ToList();
            return result;
        }
        public List<CommentDTO> GetCreatorEventComment(string loginemailid)
        {
            var result = _obj.Comments.Select(x => new CommentDTO()
            {
                EventID = x.EventID,



                Comment1 = x.Comment1,



                Email = x.Email,



                CreatorID = x.CreatorID,
                EventName = x.EventName
            }).Where(x => x.CreatorID == loginemailid).ToList();
            return result;
        }
    }
}
