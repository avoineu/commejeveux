namespace School.ViewModels;

public abstract class Evaluations
{
    private Activity activity;

    public Evaluations(Activity activity) {
        this.activity = activity;
    }

    public Activity Activity {
        get {
            return activity;
        }
    }

    public abstract int Note();

    public override string ToString()
    {
        return String.Format("{0}: {1}/20", Activity, Note());
    }
}
