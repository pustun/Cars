using System;

namespace WebApi.Services
{
    public interface ICalendar
    {
        DateTime Today { get; }
    }
}
