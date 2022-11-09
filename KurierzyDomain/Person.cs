namespace KurierzyDomain
{
    public class Person
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string? City { get; set; }
        public string passwordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public override string ToString()
        {
            return ("Person " + Id + " " + Email + " "+ Name + " " + Surname+ " "+Birthday + " "+ City+
                " " + passwordHash + " " + RoleId);
        }
    }
}


