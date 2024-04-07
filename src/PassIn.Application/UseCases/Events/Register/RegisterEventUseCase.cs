using PassIn.Communication.Requests;

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
            throw new Exception("Maximum attendees must be greater than 0.");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new Exception("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Details))
            throw new Exception("Name is required.");
    }
}
