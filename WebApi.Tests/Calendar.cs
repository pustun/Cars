using System;
using WebApi.Services;

namespace WebApi.Tests
{
    public class Calendar : ICalendar
    {
        public DateTime Today => DateTime.Today;
    }
}
