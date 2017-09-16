using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Fuel Fuel { get; set; }

        public int Price { get; set; }

        public bool New { get; set; }

        public int? Mileage { get; set; }

        public DateTime? FirstRegistration { get; set; }
    }

    public enum Fuel
    {
        None = 0,
        Gasoline = 1,
        Diesel = 2
    }
}
