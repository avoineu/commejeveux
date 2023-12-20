using System.Diagnostics;
using System.Collections.ObjectModel;
using School.Models;
namespace School.Views;

public partial class EvaluationsPage : ContentPage
{
	public ObservableCollection<Etudiants> etudiantsList {get; set;} = new ObservableCollection<Etudiants>();
	public ObservableCollection<Models.Activity> activityList {get; set;} = new ObservableCollection<Models.Activity>();

	private EtudiantsPage etudiantsPage = new EtudiantsPage();
	private ActivitesPage activityPage = new ActivitesPage();
	private EnseignantsPage enseignantsPage = new EnseignantsPage();
	public EvaluationsPage()
	{
		InitializeComponent();
		BindingContext = this;
		this.enseignantsPage = enseignantsPage;

		foreach(var elem in Etudiants.LoadAll()){
			etudiantsList.Add(elem);
		}

		foreach(var ens in Models.Activity.LoadAll(enseignantsPage.enseignantsList)){
		activityList.Add(ens);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if( etudiantsPage != null){
			this.BindingContext = this;
			if( StudentPicker != null){
						StudentPicker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
						StudentPicker.IsVisible = true;
			}
			if( Student2Picker != null){
						Student2Picker.ItemsSource = etudiantsPage.etudiantsList.Select(ens => ens.DisplayName).ToList();
						Student2Picker.IsVisible = true;
			}}
		if( activityPage != null){
			this.BindingContext = this;
			if( ActivityPicker != null){
						ActivityPicker.ItemsSource = activityPage.activityList.Select(elem => elem.Code).ToList();
						ActivityPicker.IsVisible = true;
			}
			if( Activity2Picker != null){
						Activity2Picker.ItemsSource = activityPage.activityList.Select(elem => elem.Code).ToList();
						Activity2Picker.IsVisible = true;
			}}
    }

	private void OnAddCoteClicked(object sender, EventArgs e){
		string studentselected = StudentPicker.SelectedItem.ToString();
		string activityselected = ActivityPicker.SelectedItem.ToString();
		string coteselected = CotePicker.SelectedItem.ToString();
		Console.WriteLine("student :"+studentselected+" activité :"+activityselected+" cote :"+coteselected);
		Models.Activity activity2link = null;

		foreach(Models.Activity elem in activityList){
			if(Equals(activityselected,elem.Code)){
				activity2link = elem;
				Console.WriteLine("activité trouvé !");
			}
		}
		Cotes cotesadded = new Cotes(activity2link);
		cotesadded.SetNote(int.Parse(coteselected));//converti string en chiffre

		Etudiants etudiants2link = null;

		foreach(Etudiants elem in etudiantsList){
			if(Equals(studentselected,elem.DisplayName)){
				etudiants2link = elem;
				Console.WriteLine("étudiants trouvé !");
			}
		}
		DisplayAlert("Success", "Cote added successfully. It worked!", "OK");
		etudiants2link.Add(cotesadded);
		etudiants2link.Save();
		Console.WriteLine(etudiants2link.Bulletin());
	}
	private void OnAddAppreciationClicked(object sender, EventArgs e){
		string selectedstudent = Student2Picker.SelectedItem.ToString();
		string selectedactivty = Activity2Picker.SelectedItem.ToString();
		string selectedappreciation = AppreciationPicker.SelectedItem.ToString();

		Models.Activity activite2link = null;
		foreach(Models.Activity elem in activityList){
			if(Equals(selectedactivty,elem.Code)){
				activite2link = elem;
			}
		}
		Appreciation appreciationadded = new Appreciation(activite2link);
		appreciationadded.SetAppreciation(selectedappreciation);

		Etudiants student2link = null;
		foreach(Etudiants elem in etudiantsList){
			if(Equals(selectedstudent,elem.DisplayName)){
				student2link = elem;
			}
		}
		DisplayAlert("Succes","Appréciation added successfully, it works!","ok"); 
		student2link.Add(appreciationadded);
		student2link.Save();
	}
}