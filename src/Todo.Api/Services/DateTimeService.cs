namespace Todo.Api.Services;

public class DateTimeService : IDateTimeService
{
	public DateTime GetDateTimeNow()
	{
		return DateTime.UtcNow;
	}
}