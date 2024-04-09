using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;

    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }

    public ResponseRegisteredJson Execute(Guid eventId,RequestRegisterEventJson request)
    { 
        Validate(eventId, request);

        var entity = new Infrastructure.Entities.Attendee
        {
            Name = request.Name,
            Email = request.Email,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow
        };

        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson { Id = entity.Id };
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(eventId);
        if(eventEntity is null)
            throw new NotFoundException("Event not found with this id.");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new OnValidationException("Name is required.");

        if (EmailIsValid(request.Email) is false)
            throw new OnValidationException("Email is required.");

        var attendeeExist = _dbContext
            .Attendees
            .Any(attendees => attendees.Email.Equals(request.Email) && attendees.Event_Id == eventId);
        if (attendeeExist is true)
            throw new OnValidationException("Attendee already registered on this event.");

        int attendeesCount = _dbContext.Attendees.Count(attendees => attendees.Event_Id == eventId);
        if (attendeesCount > eventEntity.Maximum_Attendees - 1)
            throw new OnValidationException("Event is full.");
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }

    }
}