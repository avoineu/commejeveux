namespace School.Views;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.Models;

public partial class ActivitesPage : ContentPage
{
	private EnseignantsPage enseignantsPage = new EnseignantsPage();

	public ActivitesPage(){
		InitializeComponent();
	}
	public ActivitesPage(EnseignantsPage enseignantsPage)
	{
		InitializeComponent();
		this.enseignantsPage = enseignantsPage;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if( enseignantsPage != null){
			this.BindingContext = this;
			// Console.WriteLine("Number of teachers in the list: " + enseignantsPage.enseignantsList.Count);
			// Console.WriteLine("ListTeacher First element"+enseignantsPage.enseignantsList[0]);
			// Console.WriteLine("DIsplayName test "+enseignantsPage.enseignantsList[0].DisplayName);
			if( TeacherPicker != null){
						// Console.WriteLine("Number of teachers before setting ItemsSource: " + enseignantsPage.enseignantsList.Count);
						TeacherPicker.ItemsSource = enseignantsPage.enseignantsList.Select(ens => ens.DisplayName).ToList();
						// TeacherPicker.ItemsSource = enseignantsPage.enseignantsList; //Display the salary with it
						// Console.WriteLine("Displays teachers in the picker");
						TeacherPicker.IsVisible = true;
						// Console.WriteLine("Number of teachers after setting ItemsSource: " + TeacherPicker.ItemsSource.Count);

			}}
		// else{Console.WriteLine("EnseignantsPage.enseignantsList is null");}
    }

    private void OnAddActivityClicked(object sender, EventArgs e){
		string activityName = ActivityNameEntry.Text;
		string CodeName = ActivityCodeEntry.Text;
		int ects;

		if(int.TryParse(ActivityEctsEntry.Text, out ects)){
			string selectedTeacher = TeacherPicker.SelectedItem.ToString();
			// Enseignants selectedTeacher = new Enseignants(TeacherPicker.SelectedItem, TeacherPicker.SelectedItem);
			Enseignants teacher2 = null;
			foreach( Enseignants elem in enseignantsPage.enseignantsList){
				Console.WriteLine(Equals(selectedTeacher,elem.DisplayName));
				Console.WriteLine(elem.DisplayName);
					if(Equals(selectedTeacher,elem.DisplayName)){
						teacher2 = elem ;
					}
			}
			// Console.WriteLine("Prof selectione "+ selectedTeacher );
			if(selectedTeacher != null){
				Activity newactivity = new Activity(activityName,CodeName,ects, teacher2);

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