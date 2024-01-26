using Microsoft.Maui.Controls.Compatibility.Platform;

namespace School.Models;

public class Enseignants : Persons
{
    private int salary;

    public string Filename {get;set;}

    public Enseignants(string firstname, string lastname, int salary) : base(firstname, lastname) {
        this.salary = salary;
        string namestockfile = lastname + firstname;
        Filename = $"{namestockfile}.notes.txt";
    }

    public override string ToString()
    {
        return String.Format("{0} ({1})", DisplayName, salary);
        //return DisplayName
    }

    public int Salary {
        get {
            return salary;
        }
    }

    public void Save() {
        string content = String.Format("{0}\n{1}\n{2}", Firstname, Lastname, Salary);
        File.WriteAllText(System.IO.Path.Combine(Config.RootDir,"Enseignants", Filename), content);
    }

    public static Enseignants Load(string filename)
        {
            
            filename = System.IO.Path.Combine(Config.RootDir,"Enseignants", filename);
            Console.WriteLine(filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            var content = File.ReadAllText(filename);

            var tokens = content.Split('\n');


            Console.WriteLine("OK: {0}, {1}, {2}", tokens[0], tokens[1], tokens[2]);
            return
                new(tokens[0], tokens[1], Convert.ToInt32(tokens[2]))
                {
                    Filename = Path.GetFileName(filename),
                };
        }

        public static IEnumerable<Enseignants> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            string enseignantsDir = System.IO.Path.Combine(Config.RootDir, "Enseignants");

            return Directory

                    .EnumerateFiles( enseignantsDir ,"*.notes.txt")

                    .Select(filename => Enseignants.Load(Path.GetFileName(filename)))

                    .OrderByDescending(note => note.DisplayName);
        }
    }
