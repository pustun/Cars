using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model;

namespace WebApi.Services
{
    public class CarSortExpressionMapper : ICarSortExpressionMapper
    {
        private readonly Dictionary<string, Expression<Func<Car, object>>> _mappings;

        public CarSortExpressionMapper()
        {
            _mappings = new Dictionary<string, Expression<Func<Car, object>>>
            {
                { "id", car => car.Id},
                { "title", car => car.Title},
                { "price", car => car.Price},
                { "fuel", car => car.Fuel},
                { "new", car => car.New},
                { "mileage", car => car.Mileage},
                { "firstregistration", car => car.FirstRegistration}
            };
        }

        public Expression<Func<Car, object>> Map(string sort)
        {
            var key = sort.ToLowerInvariant();

            if (_mappings.ContainsKey(key))
            {
                return _mappings[key];
            }

            throw new NotImplementedException($"Expression mapping for the {sort} does not exist");
        }
    }
}