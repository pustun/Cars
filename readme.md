# It's a Demo Application, that implements simple REST api #

It uses [NancyFX](http://nancyfx.org "NancyFX") to implement REST endpoints. It uses MongoDB to store the data, there was no good reason to use Mongo, MS SQL, or something else, I just wanted to make set up procedure as simple as possible. Mongo DB allows to host database on a shared server ([Mongo lab](https://mlab.com/ "Mongo lab") in my case), so that there is no need to do anything at all, you just restore nuget packages, hit **F5** and the application works.

# Object structure

## Car ##

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

## Fuel ##

	public enum Fuel
    {
        None = 0,
        Gasoline = 1,
        Diesel = 2
    }

# Endpoints #

**GET /cars** returns all cars in the store, sorted by Id

**GET /cars?sort={field}** returns all cars in the store sorted by the field.
Possible options are id, title, fuel, price, new, mileage, firstregistration.

**GET /cars/{id}** returns a particular car with id provided in the URL. Id is a guid.

**POST /cars** creates a new car in the database. One thing to keep in mind: if you try to create an entry with existing Id it will fail.
Payload example:

	{
		Id: '10B571DB-8740-4CBE-8583-08CF0BC56C69',
		Price: 15000,
		New: false,
		Fuel: 1,
		Title: 'Audi A4',
		Mileage: 90000,
		FirstRegistration: '2008-11-21'
	}


**PUT /cars** updates an existing entry. Request payload is identical to the previous example.

**DELETE /cars/{id}** removes the entry by provided id