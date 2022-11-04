using KurierzyDomain;
using Microsoft.EntityFrameworkCore;

namespace KurierzyDB
{
    public class KurierzyDB
    {
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
        public Role Login(int id)
        {
            using (var context = new KurierzyDBContext())
            {
                OfficeWorker ow = context.OfficeWorkers.Single(ow => ow.PersonId == id);
                Person p = context.Persons.Single(p => p.Id == ow.PersonId);
                Role r = context.Roles.Single(r => r.Id == p.RoleId);
                return r;
            }
        }
    }
}