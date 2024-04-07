using PassIn.Communication.Requests;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validate(request);
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
