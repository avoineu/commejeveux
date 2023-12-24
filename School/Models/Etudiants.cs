namespace School.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

public class Etudiants : Persons
{
    private List<Evaluations> evaluations;

    public string Filename {get; set;}

    public Dictionary<string, int> Grades {get; set;}

    public Etudiants(string firstname, string lastname) : base(firstname, lastname) {
        evaluations = new List<Evaluations>();
        string namestockfile = lastname + firstname;
        Filename = $"{namestockfile}.notes.txt";
        Grades = new Dictionary<string, int>();
    }

    public void Add(Evaluations evaluation) {
        Console.WriteLine("eval: {0}", evaluation);
        evaluations.Add(evaluation);
        Console.WriteLine("méthode add");
        Console.WriteLine("evaluation de add : "+evaluations);
    } //quand on ajoute evaluation on doit utiliser ayoub.add(16) 

    public double Average() {
        int total = 0;
        int ects = 0;
        foreach(var evaluation in evaluations) {
            total += evaluation.Note() * evaluation.Activity.ECTS;
            ects += evaluation.Activity.ECTS;
        }
        return total/ects;
    }

    public string Bulletin() {
        var lines = new List<String>
        {
            String.Format("Bulletin de {0}", DisplayName)
        };

        foreach(var evaluation in evaluations) {
            lines.Add(evaluation.ToString());
        }
        Console.WriteLine("coucou2");
        Console.WriteLine("jy crois pas trop "+evaluations.Count());
        lines.Add(String.Format("Moyenne: {0}", Average()));
        Console.WriteLine("méthode bulletin");

        return String.Join("\n", lines);
    }

    public override string ToString()
    {
        return String.Format("{0}",DisplayName);
    }

    public void Save(){
        Console.WriteLine("evaluation de save : "+evaluations);
        if(evaluations.Count > 0) {
            Console.WriteLine("yop");
            Console.WriteLine("truc: {0}", evaluations[0]);
        }
        
        string evaluationsContent = string.Join("\n", evaluations.Select(evaluation => evaluation.ToString()));
        Console.WriteLine("evaluation content : "+evaluationsContent);
        string content = String.Format("{0}\n{1}\n{2}", Firstname, Lastname,evaluationsContent);
        File.WriteAllText(System.IO.Path.Combine(Config.RootDir,"Etudiants", Filename), content);
        Console.WriteLine("méthode save");
        Console.WriteLine(evaluations);
    }

    public static Etudiants Load(string filename){
        filename = System.IO.Path.Combine(Config.RootDir,"Etudiants", filename);
        var grades = new Dictionary<string, int>();

        if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);
        
        var content = File.ReadAllText(filename);
        var tokens = content.Split('\n');

        if (tokens.Length > 2)
                {
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(tokens[i]) && tokens[i].Contains(":"))
                        {
                            var evaluationTokens = tokens[i].Split(':');
                            var nameMatch = Regex.Match(evaluationTokens[0], @"\[(.*?)\]");

                            if (nameMatch.Success)
                            {
                                var nomevaluation = nameMatch.Groups[1].Value.Trim();
                                var note = int.Parse(evaluationTokens[1].Split('/')[0].Trim());

                                grades[nomevaluation] = note;
                                Console.WriteLine("bon début");
                            }
                        }
                    }
                }
        return
                new(tokens[0], tokens[1])
                {
                    Filename = Path.GetFileName(filename),
                    Grades =  grades,
                };
    }

    public static IEnumerable<Etudiants> LoadAll(){
        string appDataPath = FileSystem.AppDataDirectory;
        string etudiantsdir =  System.IO.Path.Combine(Config.RootDir, "Etudiants");

        return Directory
                    .EnumerateFiles(etudiantsdir ,"*.notes.txt")
                    .Select(filename => Etudiants.Load(Path.GetFileName(filename)))
                    .OrderByDescending(note => note.DisplayName);
    }
}