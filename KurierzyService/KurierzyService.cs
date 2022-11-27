using KurierzyDomain;
using KurierzyDTOs;
using System.Net.Mail;
using System.Net;

namespace KurierzyService
{
    public class KurierzyService : IKurierzyService
    {
        KurierzyDB.PersonsDB kdb = new KurierzyDB.PersonsDB();
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
            bool sendConfirmationEmails = false;
            string message = kdb.AddPerson(p);
            if (message == "success" && sendConfirmationEmails)
            {
                Console.WriteLine("Sending e-mail to admin");
                var from = "system@example.com";
                var to = "admin@example.com";
                var subject = "User added";
                var body = "Added user " + p.Name;

                var host = "smtp.mailtrap.io";
                var port = 2525;

                var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential("4f5bba2f625a63", "7880b8edfb6f82"),
                    EnableSsl = true
                };

                client.Send(from, to, subject, body);

                Console.WriteLine("Email sent");
            }
            return message;
        }
        public List<Person> getAll()
        {
            return kdb.getAll();
        }

        public Person getOne(int id)
        {
            return kdb.getOne(id);
        }
        public Person LoginPerson(LoginPersonDTO lp)
        {
            return kdb.getPersonPasswordHash(lp.Email);
        }

        public string deletePerson(int id)
        {
            return kdb.deletePerson(id);
        }
    }
}