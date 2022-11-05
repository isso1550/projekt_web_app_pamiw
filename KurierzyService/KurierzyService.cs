using KurierzyDomain;
using KurierzyDTOs;

namespace KurierzyService
{
    public class KurierzyService
    {
        public string RegisterPerson(RegisterPersonDTO p)
        {
            Person newPerson = new Person
            {
                Email = p.Email,
                Name = p.Name,
                Surname = p.Surname,
                Birthday = p.Birthday,
                City = p.City,
                passwordHash = p.Password,
                RoleId = p.RoleId,

            };
            KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
            string message = kdb.AddPerson(newPerson);
            return message;
        }
        public List<Person> getAll()
        {
            KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
            return kdb.getAll();
        }
        public Person LoginPerson(LoginPersonDTO lp)
        {
            KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
            return kdb.getPersonPasswordHash(lp.Email);
        }
    }
}