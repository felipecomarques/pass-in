using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventsId;
public class GetAllByEventsIdUseCase
{
    private readonly PassInDbContext _dbContext;

    public GetAllByEventsIdUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        //var attendees = _dbContext.Attendees.Where(attendee => attendee.Event_Id == eventId).ToList();
        var attendees = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(ev => ev.Id == eventId);
        if (attendees is null)
            throw new NotFoundException("Attendees not found with this event id.");
        return new ResponseAllAttendeesJson
        {
            Attendees = attendees.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at
            }).ToList()
        };
    }
}
