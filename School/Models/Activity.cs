namespace School.Models;

public class Activity
{
    private Enseignants teacher;
    private int ects;
    private string name;
    private string code;

    public Activity(string name, string code, int ECTS, Enseignants teacher) {
        this.code = code;
        this.teacher = teacher;
        this.name = name;
        this.ects = ECTS;
    }

    public string Code {
        get {
            return code;
        }
    }

    public string Name {
        get {
            return name;
        }
    }

    public int ECTS {
        get {
            return ects;
        }
    }

    public Enseignants Enseignants {
        get {
            return teacher;
        }
    }

    public override string ToString()
    {
        return String.Format("[{0}] {1} ({2})", Code, Name, Enseignants.DisplayName);
    }
}
