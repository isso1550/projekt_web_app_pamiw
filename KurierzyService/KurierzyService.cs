using KurierzyDomain;
using KurierzyDTOs;

namespace KurierzyService
{
    public class KurierzyService
    {
        KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
        public string ModifyPerson(int id, ModifyPersonDTO p)
        {
            return kdb.ModifyPerson(id, p);
        }

        /*public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string? City { get; set; }
        public string passwordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }*/
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
           
            return  kdb.AddPerson(newPerson);
        }
        public List<Person> getAll()
        {
            return kdb.getAll();
        }
        public Person LoginPerson(LoginPersonDTO lp)
        {
            return kdb.getPersonPasswordHash(lp.Email);
        }
    }
}