using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.EntityFrameworkCore;

namespace KurierzyDB
{
    public class KurierzyDB
    {
        public string AddPerson(Person newPerson)
            //TODO zamienic na DTO?
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
        public string ModifyPerson(int id, ModifyPersonDTO modified)
        {
            using (var context = new KurierzyDBContext())
            {
                Person p = new Person() { Id = id };
                context.Attach(p);
                p.Email = modified.Email;
                p.Name = modified.Name;
                p.Surname = modified.Surname;
                p.Birthday = modified.Birthday;
                p.City = modified.City;
                p.RoleId = modified.RoleId;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
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