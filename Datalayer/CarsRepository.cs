using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Datalayer
{
    public class CarsRepository : ICarsRepository
    {
        static CarsRepository()
        {
            BsonClassMap.RegisterClassMap<Car>(cm =>
            {
                cm.MapIdProperty(x => x.Id)
                  .SetIdGenerator(GuidGenerator.Instance);
                cm.MapProperty(x => x.Title);
                cm.MapProperty(x => x.Price);
                cm.MapProperty(x => x.Fuel);
                cm.MapProperty(x => x.New);
                cm.MapProperty(x => x.Mileage);
                cm.MapProperty(x => x.FirstRegistration);
            });
        }

        public void Add(Car car)
        {
            var carsCollection = GetCarsCollection();
            carsCollection.InsertOne(car);
        }

        private static IMongoCollection<Car> GetCarsCollection()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["cars_db"].ConnectionString);
            var database = client.GetDatabase("cars");
            return database.GetCollection<Car>("Car");
        }

        public void Update(Car car)
        {
            var carsCollection = GetCarsCollection();
            carsCollection.ReplaceOne(x => x.Id == car.Id, car);
        }

        public void Delete(Guid id)
        {
            var carsCollection = GetCarsCollection();
            carsCollection.DeleteOne(x => x.Id == id);
        }

        public Car GetById(Guid id)
        {
            var carsCollection = GetCarsCollection();

            // ReSharper disable once ReplaceWithSingleCallToFirstOrDefault
            return carsCollection
                .Find(x => x.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Car> GetAll(Expression<Func<Car, object>> sortExpression)
        {
            var carsCollection = GetCarsCollection();
            return carsCollection
                .Find(x => true)
                .SortBy(sortExpression)
                .ToList();
        }
    }
}
