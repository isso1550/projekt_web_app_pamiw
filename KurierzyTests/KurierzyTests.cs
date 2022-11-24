using Bogus;
using KurierzyDomain;
using KurierzyDB;
using Microsoft.EntityFrameworkCore;

namespace KurierzyTests
{
    public class KurierzyTests
    {
        static void Main(string[] args)
        {
            var city = new[] { "Warszawa", "Kraków", "Wrocław" }; 
            Console.WriteLine("Hello");

            var fakePersons = new Faker<KurierzyDomain.Person>("pl")
                .RuleFor(p => p.Name, f => f.Name.FirstName())
                .RuleFor(p => p.Surname, f => f.Name.LastName())
                .RuleFor(p => p.Email, (f, p) => p.Name + "." + p.Surname + "@mail.com")
                .RuleFor(p => p.Birthday, f => f.Date.Recent(0))
                .RuleFor(p => p.City, f => f.PickRandom(city))
                .RuleFor(p => p.passwordHash, f => f.Random.Hash())
                .RuleFor(p => p.RoleId, f => f.Random.Number(1, 2));
                
            var data = fakePersons.Generate(15).ToArray();

            Console.WriteLine(data[0].ToString());

            var dbc = new KurierzyDBContext();
            dbc.Persons.AddRange(data);
            dbc.SaveChanges();

        }
    }
}