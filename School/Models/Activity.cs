using School.Views;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Compatibility.Platform;

namespace School.Models;

public class Activity
{
    private Enseignants teacher;
    private int ects;
    private string name;
    private string code;
    public string Filename{get;set;}

    public Activity(string name, string code, int ECTS, Enseignants teacher) {
        this.code = code;
        this.teacher = teacher;
        this.name = name;
        this.ects = ECTS;
        string activitenamestockfile = code + name;
        Filename = $"{activitenamestockfile}.notes.txt";
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

    public void Save(){
        string content = String.Format("{0}\n{1}\n{2}\n{3}\n",name,code,ECTS,teacher);
        File.WriteAllText(System.IO.Path.Combine(Config.RootDir,"Activites",Filename), content);
    }

    public static Activity Load( string Filename, ObservableCollection<Enseignants> enseignantsList){

        Filename = System.IO.Path.Combine(Config.RootDir,"Activites",Filename);

        if (!File.Exists(Filename))
            throw new FileNotFoundException("Unable to find file on local storage.",Filename);

        var content = File.ReadAllText(Filename);
        var tokens = content.Split('\n');
        Enseignants teacher2link = null;

        Console.WriteLine("Activité chargé : {0} {1} {2} {3}", tokens[0], tokens[1], tokens[2], tokens[3]);

        foreach(Enseignants elem in enseignantsList){
            if(Equals(tokens[3],elem.DisplayName)){
                teacher2link = elem;
            }
        }
        return 
            new(tokens[0], tokens[1], Convert.ToInt32(tokens[2]),teacher2link)
            {
                Filename = Path.GetFileName(Filename),
            };
        }

        public static IEnumerable<Activity> LoadAll(ObservableCollection<Enseignants> enseignantsList){

        string appDataPath = FileSystem.AppDataDirectory;
        string activiteDirection = System.IO.Path.Combine(Config.RootDir,"Activites");
        Console.WriteLine("je suis passé par la");

        return Directory

                .EnumerateFiles(activiteDirection,"*notes.txt")

                .Select(Filename => Activity.Load(Path.GetFileName(Filename),enseignantsList))

                .OrderByDescending(note => note.ECTS);
    }
}