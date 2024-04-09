using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.DoAttendeesCheckIn;
public class DoAttendeesCheckInUseCase
{
    private readonly PassInDbContext _dbContext;

    public DoAttendeesCheckInUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);
        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow
        };
        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();
        return new ResponseRegisteredJson { Id = entity.Id };
    }

    private void Validate(Guid attendeeId)
    {
        var attendeeExist = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);
        if (attendeeExist is false)
            throw new NotFoundException("Attendee not found.");

        var checkInExist = _dbContext.CheckIns.Any(checkIn => checkIn.Attendee_Id == attendeeId);
        if (checkInExist)
            throw new ConflictException("Attendee already checked in.");
    }
}
