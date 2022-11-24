using KurierzyDomain;
using KurierzyDTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace KurierzyDB
{
    public class KurierzyDB
    {
        public string AddPerson(RegisterPersonDTO p)
            //TODO zamienic na DTO?
        {
            using(var context = new KurierzyDBContext())
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

                context.Persons.Attach(p);
                
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
                foreach (var p in context.Persons.ToList<Person>())
                {
                    l.Add(p);
                }
                return l;

            }
        }

        public Person getOne(int id)
        {
            using (var context = new KurierzyDBContext())
            {
                return context.Persons.FirstOrDefault(p => p.Id == id);
            }
        }

        public string deletePerson(int id)
        {
            using (var context = new KurierzyDBContext())
            {
                Person p = new Person() { Id = id };

                context.Persons.Attach(p);
                context.Persons.Remove(p);
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
        public Person getPersonPasswordHash(string email)
        {
            using (var context = new KurierzyDBContext())
            {
                return context.Persons.FirstOrDefault(p => p.Email == email);
            }
        }
    }
}