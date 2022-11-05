using KurierzyDomain;
using Microsoft.EntityFrameworkCore;

namespace KurierzyDB
{
    public class KurierzyDB
    {
        public string AddPerson(Person newPerson)
        {
            using(var context = new KurierzyDBContext())
            {
                context.Persons.Add(newPerson);
                try
                {
                    context.SaveChanges();
                } catch (DbUpdateException e) {
                    //insert error
                    return e.InnerException.Message;
                }
                
                return "success";
            }
            

        }
        public List<Person> getAll()
        {
            using (var context = new KurierzyDBContext())
            {
                List<Person> l = new List<Person>();
                foreach (var ow in context.OfficeWorkers.ToList<OfficeWorker>())
                {
                    Person p = context.Persons.Single(p => p.Id == ow.PersonId);
                    p.Role = context.Roles.Single(r => r.Id == p.RoleId);
                    l.Add(p);
                }
                return l;

            }
        }
        public Person getPersonPasswordHash(string email)
        {
            using (var context = new KurierzyDBContext())
            {
                return context.Persons.FirstOrDefault(p => p.Email == email);
            }
        }
    }
}