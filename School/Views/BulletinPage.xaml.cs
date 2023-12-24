using System.Collections.ObjectModel;
using School.Models;

namespace School.Views;

public partial class BulletinPage : ContentPage
{
	public ObservableCollection<Etudiants> etudiantsList {get;set;} = new ObservableCollection<Etudiants>();
	private EtudiantsPage etudiantsPage = new EtudiantsPage();
	private ActivitesPage activitesPage = new ActivitesPage();

	public BulletinPage()
	{
		InitializeComponent();

		BindingContext = this;
		
		foreach(var elem in Etudiants.LoadAll()){
			etudiantsList.Add(elem);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		etudiantsPage.LoadEtudiantsList();
		activitesPage.LoadActivityList();
		if(etudiantsPage != null){
			this.BindingContext = this ;
			if(StudentPicker != null){
				StudentPicker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
				StudentPicker.IsVisible = true;
			}
		}
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		etudiantsPage.LoadEtudiantsList();
		activitesPage.LoadActivityList();
    }

	private void OnAddBulletinClicked(object sender, EventArgs e){
		string selectedstudent = StudentPicker.SelectedItem.ToString();
		Etudiants etudiant2link = null;

		foreach(Etudiants elem in etudiantsList){
			if(Equals(selectedstudent,elem.DisplayName)){
				etudiant2link = elem;
			}
		}

		var lines = new List<String>
        {
            String.Format("Bulletin de {0}", etudiant2link.DisplayName)
        };

		Dictionary<string, int> bulleting = etudiant2link.Grades;
		int totalECTS = 0;
		int total = 0;
		foreach (var grade in bulleting){
			Console.WriteLine("on est dans le dico");
			Console.WriteLine($"Activity Code: {grade.Key}, Note: {grade.Value}");
			Models.Activity activityFound = null;
			foreach (Models.Activity activity in activitesPage.activityList){
                 if (Equals(grade.Key, activity.Code)){
                    activityFound = activity;
					total += grade.Value * activityFound.ECTS;
					totalECTS += activityFound.ECTS;
					lines.Add($"[{activityFound.Code}] Cours de {activityFound.Name} : {grade.Value}/20   ({activityFound.ECTS} ECTS)");
					Console.WriteLine("TROUVER");}}
		}
		int average = total/totalECTS;
		lines.Add($"Moyenne : {average}");
		var Bulletin = String.Join("\n", lines);
		Console.WriteLine(Bulletin);
		DisplayAlert("Success", "Le bulletin a bien été affiché", "OK");
		BulletinLabel.Text = Bulletin;
	}
}