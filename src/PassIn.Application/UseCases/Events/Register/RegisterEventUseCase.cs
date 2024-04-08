using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public ResponseRegisteredEventJson Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();
        var entity = new Infrastructure.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")
        };

        dbContext.Events.Add(entity);
        dbContext.SaveChanges();

        return new ResponseRegisteredEventJson { Id = entity.Id };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees < 1)
            throw new PassInException("Maximum attendees must be greater than 0.");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new PassInException("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Details))
            throw new PassInException("Name is required.");
    }
}
