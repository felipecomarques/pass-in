using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid eventId)
    {
        var dbContext = new PassInDbContext();
        var entity = dbContext.Events.FirstOrDefault(ev => ev.Id == eventId);

        if (entity is null)
            throw new PassInException("Event not found with this id.");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = -1
        };
    }
}
