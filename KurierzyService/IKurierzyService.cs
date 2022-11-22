using KurierzyDomain;
using KurierzyDTOs;

public interface IKurierzyService
{
    string ModifyPerson(int id, ModifyPersonDTO p);
    string RegisterPerson(RegisterPersonDTO p);
    List<Person> getAll();

    Person LoginPerson(LoginPersonDTO lp);
}