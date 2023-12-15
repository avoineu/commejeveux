using Microsoft.Maui.Controls.Compatibility.Platform;

namespace School.Models;

public class Enseignants : Persons
{
    private int salary;

    public string Filename {get;set;}

    public Enseignants(string firstname, string lastname, int salary) : base(firstname, lastname) {
        this.salary = salary;
        Filename = $"{Path.GetRandomFileName()}.notes.txt";
    }

    public override string ToString()
    {
        return String.Format("{0} ({1})", DisplayName, salary);
    }

    public int Salary {
        get {
            return salary;
        }
    }

    public void Save() {
        string content = String.Format("{0}\n{1}\n{2}", Firstname, Lastname, Salary);
        File.WriteAllText(System.IO.Path.Combine(Config.RootDir, Filename), content);
    }

    public static Enseignants Load(string filename)
        {
            filename = System.IO.Path.Combine(Config.RootDir, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            var content = File.ReadAllText(filename);

            var tokens = content.Split();



            return
                new(tokens[0], tokens[1], Convert.ToInt32(tokens[2]))
                {
                    Filename = Path.GetFileName(filename),
                };
        }

        public static IEnumerable<Enseignants> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(Config.RootDir, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => Enseignants.Load(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by name
                    .OrderByDescending(note => note.DisplayName);
        }
    }
