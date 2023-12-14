namespace School.ViewModels;

public class Enseignants : Persons
{
    private int salary;
    public Enseignants(string firstname, string lastname, int salary) : base(firstname, lastname) {
        this.salary = salary;
    }

    public override string ToString()
    {
        return String.Format("{0} ({1})", DisplayName(), salary);
    }

    public int Salary {
        get {
            return salary;
        }
    }
}
