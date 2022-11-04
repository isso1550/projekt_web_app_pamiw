using KurierzyDomain;
using KurierzyDB;

namespace KurierzyService
{
    public class KurierzyService
    {
        public List<Person> getAll()
        {
            KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
            return kdb.getAll();
        }
        public Role Login(int id)
        {
            KurierzyDB.KurierzyDB kdb = new KurierzyDB.KurierzyDB();
            return kdb.Login(id);
        }
    }
}