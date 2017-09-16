using System;

namespace WebApi.Services
{
    public class Calendar : ICalendar
    {
        public DateTime Today => DateTime.Today;
    }
}
