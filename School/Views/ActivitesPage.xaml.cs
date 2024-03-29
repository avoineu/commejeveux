namespace School.Views;
using System.Linq;
using School.Models;
using System.Collections.ObjectModel;

public partial class ActivitesPage : ContentPage
{
	private EnseignantsPage enseignantsPage = new EnseignantsPage();

	public ObservableCollection<Enseignants> enseignantsList {get; set;} = new ObservableCollection<Enseignants>();

	public ObservableCollection<Activity> activityList {get; set;} = new ObservableCollection<Activity>();

	public ActivitesPage(){
		InitializeComponent();
		BindingContext = this;

		LoadActivityList();
	}

	public void LoadActivityList(){
		activityList.Clear();
		foreach(var ens in Activity.LoadAll(enseignantsList)){
		activityList.Add(ens);
		}
	}
	public ActivitesPage(EnseignantsPage enseignantsPage)
	{
		InitializeComponent();

		BindingContext = this;
		this.enseignantsPage = enseignantsPage;

		foreach(var ens in Activity.LoadAll(enseignantsPage.enseignantsList)){
		activityList.Add(ens);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		enseignantsPage.LoadEnseignantsList();
		if( enseignantsPage != null){
			this.BindingContext = this;
			if( TeacherPicker != null){
						TeacherPicker.ItemsSource = enseignantsPage.enseignantsList.Select(ens => ens.DisplayName).ToList();
						TeacherPicker.IsVisible = true;
			}}
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		enseignantsPage.LoadEnseignantsList();
    }

    private void OnAddActivityClicked(object sender, EventArgs e){
		string activityName = ActivityNameEntry.Text;
		string CodeName = ActivityCodeEntry.Text;
		int ects;

		if(int.TryParse(ActivityEctsEntry.Text, out ects)){
			string selectedTeacher = TeacherPicker.SelectedItem.ToString();
			Enseignants teacher2link = null;
	
			foreach( Enseignants elem in enseignantsPage.enseignantsList){

				Console.WriteLine(Equals(selectedTeacher,elem.DisplayName));
				Console.WriteLine(elem.DisplayName);

					if(Equals(selectedTeacher,elem.DisplayName)){
						teacher2link = elem ;
					}
			}
			if(selectedTeacher != null){
				Activity newactivity = new Activity(activityName, CodeName, ects, teacher2link);
				newactivity.Save();
				activityList.Add(newactivity);

				DisplayAlert("Success","Activity added succesfully","ok");
				ActivityNameEntry.Text = string.Empty;
				ActivityCodeEntry.Text = string.Empty;
				ActivityEctsEntry.Text = string.Empty;
			}
			else{
				DisplayAlert("Error","Teacher not selected","ok");
			}
		}
		else{
			DisplayAlert("Error","Invalid ECTS input","ok");
		}
	}
}